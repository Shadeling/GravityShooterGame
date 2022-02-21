using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int KeyType = 0;
    [SerializeField] int KeyCount = 1;


    private MyAudioManager audioManager;


    private void Start()
    {
        audioManager = FindObjectOfType<MyAudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            audioManager.PlayKeyPickUpSound();
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
