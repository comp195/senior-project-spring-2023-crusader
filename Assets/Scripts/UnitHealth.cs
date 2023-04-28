using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth
{
    // Fields
    private int current_health;
    private int current_max_health;

    // Properties
    public int Health
    {
        get
        {
            return current_health;
        }
        set
        {
            current_health = value;
        }
    }

    public int MaxHealth
    {
        get
        {
            return current_max_health;
        }
        set
        {
            current_max_health = value;
        }
    }

    // Constructor
    public UnitHealth(int health, int max_health)
    {
        current_health = health;
        current_max_health = max_health;
    }

    // Methods

    public void DamageUnit(int damageAmount)
    {
        if(current_health > 0)
        {
            current_health -= damageAmount;
        }
    }

    public void HealUnit(int healAmount)
    {
        if(current_health < current_max_health)
        {
            current_health += healAmount;
        }
        if(current_health > current_max_health)
        {
            current_health = current_max_health;
        }
    }
}
