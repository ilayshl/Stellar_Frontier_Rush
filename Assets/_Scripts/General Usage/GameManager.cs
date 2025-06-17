using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Action<GameState> OnGameStateChanged;
    public GameState state;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }


    public void ChangeGameState(GameState newState = GameState.Active)
    {
        switch (newState)
        {
            case GameState.Active:

                break;
            case GameState.Paused:

                break;
            case GameState.Dead:

                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }
}
