using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    public delegate void HealthEventDelegate(HealthComponent parent);
    public delegate void HealthEventDeltaDelegate(HealthComponent parent,float delta);


    private HealthEventDelegate OnDamage = null;
    private HealthEventDelegate OnHeal = null;
    private HealthEventDeltaDelegate OnDelta = null;

    float _Health;
    [SerializeField] private float StartingHealth = 0f;
    [SerializeField] private float _MaxHealth = 100f;

    public float Health{get=>_Health;}
    public float MaxHealth { get => _MaxHealth; }

    private void Awake()
    {
        _Health = StartingHealth;
    }

    private void SetHealth_Internal(float healthValue)
    {
        float OldHealth = _Health;
        _Health = Mathf.Clamp(healthValue,0,_MaxHealth);
        OnDelta(this,OldHealth-_Health);
    }

    public void Damage(float damage)
    {
        OnDamage(this);
        SetHealth_Internal(_Health - damage);
    }

    public void Heal(float regen)
    {
        OnHeal(this);
        SetHealth_Internal(_Health + regen);
    }
}
