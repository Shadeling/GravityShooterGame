using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityUpdate : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Image GravityBar;

    void Start()
    {
        playerController.GravityChanged += OnGravityChange;
        OnGravityChange(playerController.GetGravityCoeff());
    }

    private void OnGravityChange(float grav)
    {
        switch (grav)
        {
            case 1: GravityBar.fillAmount = 0.32f; return;
            case 3: GravityBar.fillAmount = 0.66f; return;
            case 9: GravityBar.fillAmount = 1f; return;
        }
    }

}
