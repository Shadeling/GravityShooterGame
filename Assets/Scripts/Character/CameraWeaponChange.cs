using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWeaponChange : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject GravityGun;
    [SerializeField] GameObject HandGun;

    void Start()
    {
        
    }

    public void WeaponEquip(Weapons weapon)
    {
        GravityGun.SetActive(false);
        HandGun.SetActive(false);

        switch (weapon)
        {
            case Weapons.GravityGun: GravityGun.SetActive(true); break;
            case Weapons.Handgun: HandGun.SetActive(true); break;
        }
    }

    public Vector3 GetWeaponTransform(Weapons weapon)
    {
        switch (weapon)
        {
            case Weapons.GravityGun:
                return GravityGun.transform.position + gameObject.transform.forward/2 + gameObject.transform.up/5;

            case Weapons.Handgun: 
                return HandGun.transform.position + gameObject.transform.forward / 2 + gameObject.transform.up / 5;
        }
        return transform.position;
    }

    public GameObject GetWeapon(Weapons weapon)
    {
        switch (weapon)
        {
            case Weapons.GravityGun:
                return GravityGun;

            case Weapons.Handgun:
                return HandGun;

            default: return null;
        }
    }

    /*public void SetWeaponTransform(Weapons weapon, Transform transform)
    {
        switch (weapon)
        {
            case Weapons.GravityGun: GravityGun.transform.;

            case Weapons.Handgun: return HandGun.transform.position;
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
