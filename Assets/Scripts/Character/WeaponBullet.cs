using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 _direction = Vector3.zero;
    private float _speed = 0;
    private float _damage = 0;

    void Start()
    {
        
    }

    public void Init(Vector3 _direction, float _speed, float _damage)
    {
        this._direction = _direction;
        this._speed = _speed;
        this._damage = _damage;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(_direction * _speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //var enemyHP = collision.gameObject.GetComponentInParent<EnemyHealth>();
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out var enemyHP))
        {
            //Debug.Log("Enemy take damage");
            enemyHP.TakeDamage(_damage);
        }

        if (collision.gameObject.CompareTag("Player")) return;

        Destroy(this.gameObject);
    }

}
