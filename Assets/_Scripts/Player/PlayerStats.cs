using System;
using System.Collections.Generic;
using System.Linq;

public class PlayerStats
{
    public Dictionary<StatType, float> stats { get; private set; }
    private int initialHP;

    /*  public PlayerStats(int health, int damage, int firerate, int movespeed, int missile)
      {
          stats.Add(StatType.Health, health);
          stats.Add(StatType.Damage, damage);
          stats.Add(StatType.FireRate, firerate);
          stats.Add(StatType.MoveSpeed, movespeed);
          stats.Add(StatType.Missile, missile);
      } */

    public void ChangeStat(StatType stat, float addition)
    {
        if (stats.Keys.Contains(stat) == false)
        {
            stats[stat] = addition;
            if (stat == StatType.Health)
            {
                SetInitialHP((int)addition);
            }
        }
        else
        {
            switch (stat)
            {
                case StatType.Health:
                    ChangeHealth((int)addition);
                    break;
                case StatType.Damage:
                    ChangeDamage(addition);
                    break;
                case StatType.FireRate:

                    break;
                case StatType.MoveSpeed:

                    break;
                case StatType.Missile:

                    break;
            }
        }
    }

    private void SetInitialHP(int value)
    {
        initialHP = value;
    }

    /// <summary>
    /// Increases Health amount by an int, but can't surpass the initial Health amount.
    /// </summary>
    /// <param name="value"></param>
    private void ChangeHealth(float value)
    {
        //playerBase.ChangeHealth(value);
    }

    /// <summary>
    /// Increases the damage impact of the object's bullets.
    /// </summary>
    /// <param name="increase"></param>
    private void ChangeDamage(float increase)
    {
        stats[StatType.Damage] += increase;
    }

    /// <summary>
    /// Percentage of how faster the player will shoot; lower shootInterval = faster fire rate.
    /// </summary>
    /// <param name="increase"></param>
    private void ChangeFireRate(float increase)
    {
        float bonusPercentage = 1 - (increase / 100);
        stats[StatType.FireRate] *= bonusPercentage;
        stats[StatType.FireRate] = UnityEngine.Mathf.Max(0.1f, stats[StatType.FireRate]);
        float dps = 1 / stats[StatType.FireRate];
        dps = (float)Math.Round(dps, 2);
    }

    /// <summary>
    /// Increases moveSpeed by the amount given.
    /// </summary>
    /// <param name="increase"></param>
    private void ChangeSpeed(float increase)
    {
        stats[StatType.MoveSpeed] += increase;
    }

    /// <summary>
    /// Increases the missile ammo by the amount given.
    /// </summary>
    /// <param name="value"></param>
    private void ChangeMissileAmmo(float value)
    {
        stats[StatType.Missile] += value;
    }
    
}
