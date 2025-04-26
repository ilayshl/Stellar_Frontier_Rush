using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    /*public Dictionary<StatType, float> stats { get; private set; }
    private HitPoints playerHealth;

    private const int PLAYER_MAXIMUM_HEALTH = 10;
    private const int PLAYER_DAMAGE = 1;
    private const float PLAYER_SHOOT_INTERVAL = 0.7f;
    private const int PLAYER_MOVE_SPEED = 5;
    private const int PLAYER_MISSILE = 0;

    public PlayerStats(StatType[] statsToAdd)
    {
        foreach(var stat in stats)
        {
            stats.Add(stat, );
        }
    }

    private void InitializeStats()
    {
        stats = new();
        playerHealth = new(PLAYER_MAXIMUM_HEALTH);
        stats.Add(StatType.Health, playerHealth.currentHP);
        stats.Add(StatType.Damage, PLAYER_DAMAGE);
        stats.Add(StatType.FireRate, PLAYER_SHOOT_INTERVAL);
        stats.Add(StatType.MoveSpeed, PLAYER_MOVE_SPEED);
        stats.Add(StatType.Missile, PLAYER_MISSILE);
    }

    /// <summary>
    /// Takes an index number of StatType enum and an increase value.
    /// </summary>
    /// <param name="indexOfEnum"></param>
    /// <param name="amount"></param>
    public void ChangeStat(StatType type, int amount)
    {
        switch (type)
        {
            case StatType.Health:
                if (amount > 0) { playerHealth.GainHealth(amount); }
                else if (amount < 0) { playerHealth.LoseHealth(amount); }
                stats[type] = playerHealth.currentHP;
                UpdateText(type);
                break;

            /**case StatType.Damage:
            bulletType += amount;
            bulletType = Mathf.Min(bulletType, shoot.BulletTypes());
            UpdateText(type);
            break;

            case StatType.FireRate:
                //For now- to make sure the increases in fire rate are persistent. 5 = amount
                float bonusPercentage = 1 - (5 / 100);
                Debug.Log("Old is " + stats[type]);
                Debug.Log("Bonus percentage is " + bonusPercentage);
                stats[type] *= bonusPercentage;
                stats[type] = Mathf.Max(0.1f, stats[StatType.FireRate]);
                Debug.Log("new is " + stats[type]);
                float dps = 1 / stats[StatType.FireRate];
                UpdateText(type, Math.Round(dps, 2).ToString());
                break;

            default:
                stats[type] += amount;
                UpdateText(type);
                break;
        }
    }

    //For fire rate- the lower fire rate the faster shooting will be. It multiplies the current fire rate by 0.05, until it reaches 0.1.

    private void UpdateText(StatType stat)
    {
        uiManager.SetText((int)stat, stats[stat].ToString());
    }

    private void UpdateText(StatType stat, string value)
    {
        uiManager.SetText((int)stat, value);
    }

    public override string ToString()
    {
        return " ";
    }

    public bool IsDead()
    {
        return playerHealth.IsDead();
    }
    */
}
