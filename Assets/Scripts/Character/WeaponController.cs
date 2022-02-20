using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapons
{
    GravityGun = 0,
    Handgun = 1
};

[RequireComponent(typeof(LineRenderer))]
public class WeaponController : MonoBehaviour
{ 
    [SerializeField] float bulletSpeed = 50;


    [SerializeField] Camera cam;
    [SerializeField] GameObject HandGunBullet;
    [SerializeField] float HandGunDamage = 10;
    [SerializeField] float HandGunTimeBetweenShots = 0.4f;

    [SerializeField] AudioClip gravLeft;
    [SerializeField] AudioClip gravRight;
    [SerializeField] AudioClip handgunShot;


    private AudioSource _audiSource;
    private Weapons weaponType = Weapons.GravityGun;
    //Скрипт на камере для обработки оружия
    private CameraWeaponChange _camWeapon;
    private LineRenderer _line;
    private float _time;


    void Start()
    {
        _line = gameObject.GetComponent<LineRenderer>();
        cam.TryGetComponent<CameraWeaponChange>(out _camWeapon);
        TryGetComponent<AudioSource>(out _audiSource);
    }

    private void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeapons();


        switch (weaponType)
        {
            case Weapons.GravityGun: UseGravityGun(); return;
            case Weapons.Handgun: UseHandGun(); return;
        }
    }

    private void ChangeWeapons()
    {
        if (Input.GetKeyDown("1"))
        {
            weaponType = Weapons.GravityGun;
            _camWeapon.WeaponEquip(weaponType);
        }
        else if (Input.GetKeyDown("2"))
        {
            weaponType = Weapons.Handgun;
            _camWeapon.WeaponEquip(weaponType);
        }

        //Вызов метода для показа нужного оружия
    }

    private void UseGravityGun()
    {
        float gravityType=0;
        bool shoot=false;

        if (Input.GetButton("Fire1")) { gravityType = 1; shoot = true; }
        else if (Input.GetButton("Fire2")) { gravityType = -1; shoot = true; }
        //else if (Input.GetButton("Fire3")) { gravityType = 0; shoot = true; }

        if (shoot)
        {
            //ОТрисовка луча до цели разного цвета
            RaycastHit hit;
            //int layerMask = 1 << 8;

            if (Physics.Raycast(_camWeapon.GetWeaponTransform(weaponType), cam.transform.forward, out hit, Mathf.Infinity))
            {

                switch (gravityType)
                {
                    case 1: 
                        _line.startColor = Color.green;
                        if (!_audiSource.isPlaying)
                        {
                            _audiSource.PlayOneShot(gravLeft, 0.2f);
                        }
                        break;
                    case -1: 
                        _line.startColor = Color.red;
                        if (!_audiSource.isPlaying)
                        {
                            _audiSource.PlayOneShot(gravRight, 0.2f);
                        }
                        break;
                    case 0: _line.startColor = Color.blue; break;
                }


                _line.enabled = true;
                List<Vector3> pos = new List<Vector3>();
                pos.Add(_camWeapon.GetWeaponTransform(weaponType));
                pos.Add(hit.point);
                _line.startWidth = 0.2f;
                _line.endWidth = 0.2f;
                _line.SetPositions(pos.ToArray());
                _line.useWorldSpace = true;

                // Raycast без маски слоя нужен чтобы визуально отрисовать линию выстрела, но без маски почему-то не всегда правильно определяет цель,
                // часто игнорирует объекты с гравитацией, поэтому не смог найти другого выхода кроме как делать 2 raycast, чтобы все работало как нужно
                int layerMask = 1 << 8; //слой GravityAffected
                if(Physics.Raycast(_camWeapon.GetWeaponTransform(weaponType), cam.transform.forward, out hit, Mathf.Infinity, layerMask))
                {
                    //Debug.Log(hit.collider.gameObject.tag);
                    if (hit.collider.gameObject.TryGetComponent<GravityAffected>(out var gravity))
                    {
                        gravity.ChangeGravity(gravityType);
                    }
                }

            }
        }
        else
        {
            _line.enabled = false;
            _audiSource.Stop();
        }


    }

    private void UseHandGun()
    {
        if ((Input.GetButtonDown("Fire1") || Input.GetButton("Fire1")) && _time>HandGunTimeBetweenShots)
        {
            //Анимация выстрела
            //_camWeapon.GetWeapon(weaponType).GetComponent<Animation>().Play("Gun");
            _camWeapon.GetWeapon(weaponType).GetComponent<Animator>().SetTrigger("Shoot");
            _audiSource.PlayOneShot(handgunShot, 0.2f);

            var _bullet = Instantiate(HandGunBullet, _camWeapon.GetWeaponTransform(weaponType), Quaternion.identity);
            _bullet.GetComponent<WeaponBullet>().Init(_camWeapon.GetWeapon(weaponType).transform.forward, bulletSpeed, HandGunDamage);
            _bullet.GetComponent<Rigidbody>().AddForce(_camWeapon.GetWeapon(weaponType).transform.forward * bulletSpeed, ForceMode.VelocityChange);

            _time = 0;
        }
    }
}
