using System;
using System.Collections.Generic;
using System.Linq;

public class Stats
{
    public Dictionary<StatType, float> stats { get; private set; } = new();
    public int initialHP { get; private set; }

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
                    ChangeHealth(addition);
                    break;
                case StatType.Damage:
                    ChangeDamage(addition);
                    break;
                case StatType.FireRate:
                    ChangeFireRate(addition);
                    break;
                case StatType.MoveSpeed:
                    ChangeMoveSpeed(addition);
                    break;
                case StatType.Missile:
                    ChangeMissileAmmo(addition);
                    break;
            }
            if (stats[stat] < 0)
            {
                stats[stat] = 0;
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
        stats[StatType.Health] = Math.Min(stats[StatType.Health]+value, initialHP);
    }

    /// <summary>
    /// Increases the damage impact of the object's bullets.
    /// </summary>
    /// <param name="increase"></param>
    private void ChangeDamage(float increase)
    {
        //Hard coded 14 for now
        stats[StatType.Damage] = Math.Min(stats[StatType.Damage]+increase, 14);
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
    }

    /// <summary>
    /// Increases moveSpeed by the amount given.
    /// </summary>
    /// <param name="increase"></param>
    private void ChangeMoveSpeed(float increase)
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
