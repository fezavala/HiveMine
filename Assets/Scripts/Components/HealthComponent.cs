using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


// A reusable HealthComponent class that can go into any object that requires a health stat.
public class HealthComponent : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private int maxHP;
    [SerializeField, Range(0f, 50f)] private int defense;  // Incoming damage is subtracted by this value

    private const int MIN_HP = 0;
    private const int MIN_DAMAGE = 1;  

    private int currentHP;

    public event EventHandler<OnHPChangedEventArgs> OnHPChanged;
    public event EventHandler OnZeroHPLeft;
    public class OnHPChangedEventArgs : EventArgs  // Use EventArgs class to pass through arguments to the recievers
    {
        public int hitPoints;
    }

    private void Start()
    {
        currentHP = maxHP;
    }
    
    public int GetCurrentHP() { return currentHP; }

    public int GetMaxHP() { return maxHP; }
    public int GetDefense() { return defense; }
    public void SetDefense(int defenseValue) 
    {
        if (defenseValue < 0)
        {
            Debug.LogError("Cannot set negative defense value! " + gameObject.name);
            return;
        }

        defense = defenseValue;
    }

    public void HealHP(int healAmount)
    {
        if (healAmount < 0)
        {
            Debug.LogError(gameObject.name + " attempted to heal with a negative healAmount! healAmount should be a positive value or 0.");
            return;
        }

        currentHP = Mathf.Min(currentHP + healAmount, maxHP);

        OnHPChanged?.Invoke(this, new OnHPChangedEventArgs
        {
            hitPoints = currentHP
        });
    }

    public void DealDamage(int damageAmount, bool toolDamage = false)
    {
        if (damageAmount < 0)
        {
            Debug.LogError(gameObject.name + " was going to heal from damageAmount! Make sure the damageAmount is a positive value, not negative.");
            return;
        }

        int effectiveChange = toolDamage ? Mathf.Max(MIN_DAMAGE, damageAmount - defense) : Mathf.Max(0, damageAmount - defense);
        currentHP = Mathf.Max(MIN_HP, currentHP - effectiveChange);

        if (currentHP <= 0)
        {
            Debug.Log(gameObject.name + " has no more HP left!");
            OnZeroHPLeft?.Invoke(this, EventArgs.Empty);
            return;
        }

        OnHPChanged?.Invoke(this, new OnHPChangedEventArgs
        {
            hitPoints = currentHP
        });
    }
}
