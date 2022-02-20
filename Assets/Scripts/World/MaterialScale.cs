using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialScale: MonoBehaviour
{
    [SerializeField] float IncreaseFactor = 5;
    [SerializeField] bool Y_floors = false;
    [SerializeField] bool X_backwalls = false;
    [SerializeField] bool Z_leftwalls = false;

    //Скрипт для приведения материала к одному масштабу на объектах разных размеров и форм
    void Start()
    {
        var rend = GetComponent<Renderer>();
        if (Y_floors)
        {
            rend.material.mainTextureScale = new Vector2(transform.localScale.x / IncreaseFactor, transform.localScale.z / IncreaseFactor);
        }
        else if(X_backwalls)
        {
            rend.material.mainTextureScale = new Vector2(transform.localScale.x / IncreaseFactor, transform.localScale.y / IncreaseFactor);
        }
        else if (Z_leftwalls)
        {
            rend.material.mainTextureScale = new Vector2(transform.localScale.z / IncreaseFactor, transform.localScale.y / IncreaseFactor);
        }
    }

    void Update()
    {
        
    }

    /*private void FixedUpdate()
    {
        var rend = GetComponent<Renderer>();
        if (Y_floors)
        {
            rend.material.mainTextureScale = new Vector2(transform.localScale.x / IncreaseFactor, transform.localScale.z / IncreaseFactor);
        }
        else if (X_backwalls)
        {
            rend.material.mainTextureScale = new Vector2(transform.localScale.x / IncreaseFactor, transform.localScale.y / IncreaseFactor);
        }
        else if (Z_leftwalls)
        {
            rend.material.mainTextureScale = new Vector2(transform.localScale.z / IncreaseFactor, transform.localScale.y / IncreaseFactor);
        }
    }*/
}
