using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Health playerHealthController;
    [SerializeField] Image HealthBar;
    [SerializeField] Text Health;

    private float _maxHP;
    private float _curHP;

    void Start()
    {
        playerHealthController.onDamageTake += OnHealthUpdate;
        playerHealthController.onHeal += OnHealthUpdate;

        _maxHP = playerHealthController.GetMaxHealth();
        _curHP = playerHealthController.GetCurrentHealth();

        OnHealthUpdate(0);
    }

    private void OnHealthUpdate(float change)
    {
        //Debug.Log("HealthUpdate");
        _curHP += change;
        if (Health != null) Health.text = _curHP.ToString();

        if (HealthBar != null) HealthBar.fillAmount = _curHP / _maxHP;
    }

}
