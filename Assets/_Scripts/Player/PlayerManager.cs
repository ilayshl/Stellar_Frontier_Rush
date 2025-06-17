using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private readonly int[] DEFAULT_STATS = { 10, 1, 1, 5, 0 };

    public static PlayerManager Instance;
    public Action OnStatChanged;

    public PlayerStats playerStats;

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
        playerStats = new PlayerStats();
        for (int i = 0; i < DEFAULT_STATS.Length; i++)
        {
            playerStats.ChangeStat((StatType)i, DEFAULT_STATS[i]);
            Debug.Log($"Added {(StatType)i} to the stats with the value of {DEFAULT_STATS[i]}!");
        }
        OnStatChanged?.Invoke();
    }

    public void ChangeStat(StatType stat, int addition)
    {
        playerStats.ChangeStat(stat, addition);
        OnStatChanged?.Invoke();
        
        if (stat == StatType.Health && CheckIfDead())
        {
                GameManager.Instance.ChangeGameState(GameState.Dead);
        }
    }
    
    private bool CheckIfDead()
    {
        return playerStats.stats[StatType.Health] == 0;
    }
}
