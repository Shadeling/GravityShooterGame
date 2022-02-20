using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBullet : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 _direction = Vector3.zero;
    private float _speed = 0;
    private float _gravityType=0;

    void Start()
    {
        
    }

    public void Init(Vector3 _direction, float _speed, float _gravityType)
    {
        this._direction = _direction;
        this._speed = _speed;
        this._gravityType = _gravityType;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GravityAffected"))
        {
            collision.gameObject.GetComponentInParent<GravityAffected>().ChangeGravity(_gravityType);
        }

        if (collision.gameObject.CompareTag("Player")) return;
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}
