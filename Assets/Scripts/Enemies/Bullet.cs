using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 _direction=Vector3.zero;
    private float _speed=0;
    private float _damage=0;

    void Start()
    {

    }

    public void Init(Vector3 _direction, float _speed, float _damage)
    {
        this._direction = _direction;
        this._speed = _speed; 
        this._damage = _damage;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<Health>().Take_damage(_damage);
        }

        if (other.gameObject.CompareTag("Turret")) return;
        Debug.Log("Destroyed");
        Destroy(this.gameObject);
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<Health>().TakeDamage(_damage);
        }

        if (collision.gameObject.CompareTag("Turret")) return;
        //Debug.Log("Destroyed");
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}
