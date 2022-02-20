using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    [SerializeField] float reloadTime = 1f;
    [SerializeField] float damage = 10;

    private Animation _animation;
    private float _time;


    void Awake()
    {
        _animation = GetComponent<Animation>();
        _time = reloadTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_time > reloadTime && other.gameObject.CompareTag("Player"))
        {
            _animation.Play("Anim_TrapNeedle_Play");
            //Debug.Log("Trap Damage");
            other.gameObject.GetComponentInParent<Health>().TakeDamage(damage);
            _time = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _time+=Time.deltaTime;
    }
}
