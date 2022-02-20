using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWithCD : MonoBehaviour
{
    [SerializeField] float reloadTime = 1f;
    [SerializeField] float damage = 10;
    [SerializeField] Animation _animation;
    private float _time;

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Trap collision");
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Trap Damage");
            collision.gameObject.GetComponentInParent<Health>().TakeDamage(damage);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trap collision");
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Trap Damage");
            other.gameObject.GetComponentInParent<Health>().TakeDamage(damage);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _time += Time.deltaTime;

        if (_time > reloadTime)
        {
            _animation.Play("Anim_TrapNeedle_Play");
            _time = 0;
        }

    }
}
