using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GrenadeLauncher : MonoBehaviour
{
    [SerializeField] GameObject Bullet;

    [SerializeField] float reloadTime=200;
    [SerializeField] float damage = 10;
    [SerializeField] float grenadeSpeed= 30;
    [SerializeField] float timeToExplode= 5;

    [SerializeField] float GrenadeAdditionalUpForce= 0.5f;
    [SerializeField] float GrenadeRadius= 10;
    //[SerializeField] Collider col;


    private GameObject target= null;
    private float time=0;
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            if(time > reloadTime)
            {
                //Debug.Log("Fire");
                Fire(other);
                time = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
        }
    }

    private void RotateToPlayer(GameObject player)
    {
        //var rot = Vector3.RotateTowards(transform.forward, player.gameObject.transform.position, rotationSpeed * Time.deltaTime, 0);
        //transform.rotation = Quaternion.LookRotation(rot);
        transform.LookAt(player.transform.position);
    }

    private void Fire(Collider player)
    {
        var m_bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
        //m_bullet.GetComponent<Bullet>().Init((player.gameObject.transform.position+Vector3.up - transform.position).normalized , bulletSpeed, damage);
        
        m_bullet.GetComponent<Grenade>().Init(damage, timeToExplode, GrenadeRadius);
        Vector3 toPlayer = player.gameObject.transform.position - transform.position;

        m_bullet.GetComponent<Rigidbody>().AddForce((toPlayer + Vector3.up * toPlayer.magnitude * GrenadeAdditionalUpForce).normalized * grenadeSpeed, ForceMode.Impulse);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        time++;

        if(target!= null)
        {
            RotateToPlayer(target);
        }
    }
}
