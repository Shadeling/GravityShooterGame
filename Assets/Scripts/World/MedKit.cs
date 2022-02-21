using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class MedKit : MonoBehaviour
{
    [SerializeField] float HealAmount = 50;

    private MyAudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<MyAudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioManager.PlayMedKitPickUpSound();
            other.gameObject.GetComponentInParent<Health>().Heal(HealAmount);
            Destroy(this.gameObject);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
