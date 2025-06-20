using System;
using UnityEngine;

/// <summary>
/// Responsible for GameState.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Action<GameState> OnGameStateChanged;
    public GameState state;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        ChangeGameState(GameState.MainMenu);
    }

    public void ChangeGameState(GameState newState = GameState.Active)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Active:
                Time.timeScale = 1;
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                break;
            case GameState.Dead:
                Time.timeScale = 0.75f;
                break;
            case GameState.MainMenu:
                Time.timeScale = 1;
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                break;
        }
        Cursor.visible = false;
        OnGameStateChanged?.Invoke(newState);
    }
}
