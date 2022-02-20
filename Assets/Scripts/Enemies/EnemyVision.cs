using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyVision : MonoBehaviour
{

    public UnityAction<Vector3> PlayerInVision;
    public UnityAction<Collider> PlayerFirstTimeInVision;
    public UnityAction PlayerLost;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerFirstTimeInVision?.Invoke(other);
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInVision?.Invoke(other.transform.position);
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLost?.Invoke();
        }
    }
}
