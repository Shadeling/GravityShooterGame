using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int KeyTypeForOpen = 0;
    [SerializeField] int KeyCountForOpen = 1;
    [SerializeField] float OpeningSpeed = 0.3f;

    private bool DoorOpening = false;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponentInParent<KeyControler>().RemoveKey(KeyTypeForOpen, KeyCountForOpen)){
                DoorOpen();
                //Debug.Log("Door opened");
            }
        }
    }

    private void DoorOpen()
    {
        DoorOpening = true;
    }

    private void FixedUpdate()
    {
        if (DoorOpening)
        {
            Vector3 scale = gameObject.transform.localScale;
            if (scale.y >= OpeningSpeed)
            {
                gameObject.transform.localScale = new Vector3(scale.x, scale.y - OpeningSpeed, scale.z);
                gameObject.transform.Translate(new Vector3(0, OpeningSpeed/2, 0));
            }
            else
            {
                DoorOpening=false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
