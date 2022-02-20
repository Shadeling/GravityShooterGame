using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float DistanceX = 0;
    [SerializeField]
    private float SpeedX = 4;

    [SerializeField]
    private float DistanceZ = 0;
    [SerializeField]
    private float SpeedZ = 4;

    [SerializeField]
    private float DistanceY = 0;
    [SerializeField]
    private float SpeedY = 4;


    private int _dirX = 1;
    private int _dirZ = 1;
    private int _dirY = 1;

    private Vector3 startpos;
    private Vector3 lastpos;
    private Vector3 currentpos = Vector3.positiveInfinity;


    private GameObject Player;
    private bool isPlayerOntop = false;

    void Start()
    {
        startpos  = transform.position;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        currentpos = transform.position;

        _dirX = CheckBoundaries(currentpos.x, lastpos.x, DistanceX, startpos.x, _dirX);
        _dirZ = CheckBoundaries(currentpos.z, lastpos.z, DistanceZ, startpos.z, _dirZ);
        _dirY = CheckBoundaries(currentpos.y, lastpos.y, DistanceY, startpos.y, _dirY);

        lastpos = currentpos;
        Vector3 move = new Vector3(_dirX * SpeedX * Time.deltaTime, _dirY * SpeedY * Time.deltaTime, _dirZ * SpeedZ * Time.deltaTime);


        if(isPlayerOntop && Player != null)
        {
            Debug.Log("Player move");
            Player.transform.Translate(move, Space.World);
        }

        transform.Translate(move);
    }

    // Проверка на выход за границы движения по одной оси и возврат направления необходимого движения
    private int CheckBoundaries(float currentcoord,float lastcoord, float distance, float startcoord, int direction)
    {
        if(distance > 0)
        {
            if (currentcoord < startcoord) direction = 1;
            else if (currentcoord > startcoord + distance) direction = -1;
        }
        else if(distance < 0) 
        {
            if (currentcoord < startcoord + distance) direction = 1;
            else if (currentcoord > startcoord) direction = -1;
        }
        else
        {
            direction = 0;
        }

        //Проверка того что уперлись во что-то
        if(currentcoord == lastcoord)
        {
            direction *= -1;
        }

        return direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player");
            Player = other.gameObject;
            isPlayerOntop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player lost");
            Player = null;
            isPlayerOntop = false;
        }
    }
}
