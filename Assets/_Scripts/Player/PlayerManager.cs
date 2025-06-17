using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //0- Health, 1- Damage, 2- Firerate, 3- Movespeed, 4- Missile.
    private readonly float[] DEFAULT_STATS = { 10, 1, 0.7f, 5, 0 };

    public static PlayerManager Instance;
    public Action<StatType> OnStatChanged;

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

    public void ChangeStat(StatType stat, int addition)
    {
        playerStats.ChangeStat(stat, addition);
        OnStatChanged?.Invoke(stat);

        if (stat == StatType.Health && CheckIfDead())
        {
            GameManager.Instance.ChangeGameState(GameState.Dead);
        }
    }

    public float GetStatValue(StatType stat)
    {
        return playerStats.stats[stat];
    }

    private bool CheckIfDead()
    {
        return playerStats.stats[StatType.Health] == 0;
    }
}
