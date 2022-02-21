using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Максимальное здоровье")]
    private float maxHP = 200;

    [SerializeField]
    [Tooltip("Начальное здоровье")]
    private float startHP = 100;

    [SerializeField]
    [Tooltip("Продолжительность неуязвимости в секундах после получения урона")]
    private float immunityTime = 0.5f;

    [SerializeField] MyAudioManager audioManager;


    public UnityAction onDie;
    public UnityAction<float> onHeal;
    public UnityAction<float> onDamageTake;



    private float _currentHP { get; set; }
    private bool _invincible { get; set; }
    public bool canPickup() => _currentHP < maxHP;

    private bool _isDead;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    private void Awake()
    {
        _currentHP = startHP;
    }

    public void Heal(float heal)
    {
        float lastHP = _currentHP;
        _currentHP += heal;
        _currentHP = Mathf.Clamp(_currentHP, 0f, maxHP);
        onHeal?.Invoke(_currentHP - lastHP);
    }

    public void TakeDamage(float damage)
    {
        if (!_invincible)
        {
            audioManager.PlayHurtSound();

            float lastHP = _currentHP;
            _currentHP -= damage;
            _currentHP = Mathf.Clamp(_currentHP, 0f, maxHP);

            onDamageTake?.Invoke(_currentHP - lastHP);
            //Debug.Log(_currentHP);
            CheckDeath();

            _invincible = true;
            StartCoroutine(immunityFrame());
        }

    }

    public void Kill()
    {
        _currentHP = 0;
        CheckDeath();
    }


    public void CheckDeath()
    {
        if (_isDead)
            return;
        if (_currentHP <= 0f)
        {
            _isDead = true;
            onDie?.Invoke();
        }
    }

    public float GetCurrentHealth()
    {
        return _currentHP;
    }

    public float GetMaxHealth()
    {
        return maxHP;
    }

    IEnumerator immunityFrame()
    {
        yield return new WaitForSeconds(immunityTime);
        _invincible = false;
    }

}
