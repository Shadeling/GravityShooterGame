using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Начальное здоровье")]
    private float startHP = 100;
    [SerializeField] float armour=0;
    [SerializeField] float deathDelay = 0;


    public UnityAction onEnemyDie;
    public UnityAction<float> onEnemyDamageTaken;


    private float _currentHP { get; set; }
    private bool _invincible { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _currentHP = startHP;
    }

    public void TakeDamage(float damage)
    {
        if (!_invincible)
        {
            float lastHP = _currentHP;

            damage = Mathf.Clamp(damage, 1 , damage-armour);

            _currentHP -= damage;
            _currentHP = Mathf.Clamp(_currentHP, 0f, startHP);

            onEnemyDamageTaken?.Invoke(_currentHP - lastHP);
            CheckDeath();
        }
    }

    public void Kill()
    {
        _currentHP = 0;
        CheckDeath();
    }

    public void CheckDeath()
    {
        if (_currentHP <= 0f)
        {
            onEnemyDie?.Invoke();
            StartCoroutine(DeathDelay());
        }
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

    }
}
