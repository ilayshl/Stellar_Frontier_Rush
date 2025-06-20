using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //0- Health, 1- Damage, 2- Firerate, 3- Movespeed, 4- Missile.
    private readonly float[] DEFAULT_STATS = { 10, 1, 0.7f, 5, 0 };

    public static PlayerManager Instance;
    public Action<StatType> OnStatChanged;
    public Action<StatType, int> OnHealthChanged;

    public Stats playerStats;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        InitiateStats();
    }

    public void InitiateStats()
    {
        playerStats = new Stats();
        for (int i = 0; i < DEFAULT_STATS.Length; i++)
        {
            playerStats.ChangeStat((StatType)i, DEFAULT_STATS[i]);
            OnStatChanged?.Invoke((StatType)i);
        }
    }

    /// <summary>
    /// Changes a specific stat with the int given. If Health, checks if dead.
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="addition"></param>
    public void ChangeStat(StatType stat, int addition)
    {
        playerStats.ChangeStat(stat, addition);
        OnStatChanged?.Invoke(stat);

        if (stat == StatType.Health && addition < 0)
        {
            OnHealthChanged?.Invoke(stat, addition);
            if (CheckIfDead())
            {
                GameManager.Instance.ChangeGameState(GameState.Dead);
            }
        }

    }

    /// <summary>
    /// Returns the value of the requested stat.
    /// </summary>
    /// <param name="stat"></param>
    /// <returns></returns>
    public float GetStatValue(StatType stat)
    {
        if (playerStats.stats.ContainsKey(stat))
        {
            return playerStats.stats[stat];
        }
        else
        {
            Debug.LogWarning($"Requested stat of {stat} not found.");
            return float.NaN;
        }
    }

    private bool CheckIfDead()
    {
        return playerStats.stats[StatType.Health] == 0;
    }

    public bool IsFullHealth()
    {
        return playerStats.stats[StatType.Health] == playerStats.initialHP;
    }
}
