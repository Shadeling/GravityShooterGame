using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityAffected : MonoBehaviour
{
    // Start is called before the first frame update\
    [SerializeField] float speed = 30;
    private Rigidbody _RigidBody;
    private int steps = 50;

    void Start()
    {
        _RigidBody = gameObject.GetComponent<Rigidbody>();
        _RigidBody.freezeRotation = true;
        _RigidBody.useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _RigidBody.velocity = Vector3.zero;
    }

    public void ChangeGravity(float GravityType)
    {
        //Debug.Log("Change gravity");
        StartCoroutine(slowTranslate(GravityType));
    }

    IEnumerator slowTranslate(float GravityType)
    {
        for(int i=0; i<steps; i++)
        {
            if (GravityType > 0)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime / steps);
            }
            else if (GravityType < 0)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime / steps);
            }
            yield return new WaitForEndOfFrame();
        }
    }

}
