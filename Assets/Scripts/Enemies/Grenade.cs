using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private float _damage=0;
    private float _timeToExplode = 0;
    private float _radius = 0;

    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(Explode());
    }

    public void Init(float damage, float timeToExplode, float radius)
    {
        this._damage = damage;
        this._timeToExplode = timeToExplode;
        this._radius = radius;

        gameObject.GetComponent<SphereCollider>().radius = radius;
    }

    IEnumerator Explode()
    {
        //Debug.Log("Grenade Explode");
        yield return new WaitForSeconds(_timeToExplode);

        //explode
        //gameObject.GetComponent<SphereCollider>().
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
