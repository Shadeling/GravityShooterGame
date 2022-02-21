using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityAffected : MonoBehaviour
{
    // Start is called before the first frame update\
    [SerializeField] float speedMult = 2;
    [SerializeField] float maxSpeed = 30;
    private Rigidbody _RigidBody;
    private float _curSpeed;

    void Start()
    {
        _RigidBody = gameObject.GetComponent<Rigidbody>();
        _RigidBody.freezeRotation = true;
        _RigidBody.useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(_curSpeed > 1)
        {
            transform.Translate(Vector3.up * speedMult * Time.deltaTime * _curSpeed);
            _curSpeed -= 0.4f;
        }
        else if (_curSpeed < -1)
        {
            transform.Translate(Vector3.up * speedMult * Time.deltaTime * _curSpeed);
            _curSpeed += 0.4f;
        }

        _RigidBody.velocity = Vector3.zero;
    }

    public void ChangeGravity(float GravityType)
    {
        if (GravityType > 0 && Mathf.Abs(_curSpeed)<maxSpeed)
        {
            _curSpeed += 0.5f;
        }
        else if (GravityType < 0 && Mathf.Abs(_curSpeed) < maxSpeed)
        {
            _curSpeed -= 0.5f;
        }
    }

}
