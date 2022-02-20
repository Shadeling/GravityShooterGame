using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int KeyType = 0;
    [SerializeField] int KeyCount = 1;


    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Key Picked Up");
            other.gameObject.GetComponentInParent<KeyControler>().AddKey(KeyType, KeyCount);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
