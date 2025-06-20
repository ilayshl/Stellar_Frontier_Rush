using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Responsible for the in game canvases and sets the text in their TextMeshGUI.
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas gameplayHUD;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private TextMeshProUGUI[] textsGUI;

    private void Awake()
    {
        PlayerManager.Instance.OnStatChanged += StatChanged;
        GameManager.Instance.OnGameStateChanged += GameStateChanged;
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.OnStatChanged -= StatChanged;
        GameManager.Instance.OnGameStateChanged -= GameStateChanged;
    }

    /// <summary>
    /// Updates the stat texboxes
    /// </summary>
    /// <param name="stat"></param>
    private void StatChanged(StatType stat)
    {
        if (stat == StatType.FireRate)
        {
            float dps = 1 / PlayerManager.Instance.GetStatValue(StatType.FireRate);
            dps = (float)Math.Round(dps, 2);
            textsGUI[(int)stat].SetText(dps.ToString());
        }
        else
        {
            textsGUI[(int)stat].SetText(PlayerManager.Instance.GetStatValue(stat).ToString());
        }
    }

    private void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Active:
                gameplayHUD.gameObject.SetActive(true);
                gameOverCanvas.gameObject.SetActive(false);
                pauseCanvas.gameObject.SetActive(false);
                break;
            case GameState.Paused:
                gameplayHUD.gameObject.SetActive(false);
                pauseCanvas.gameObject.SetActive(true);
                break;
            case GameState.GameOver:
                gameplayHUD.gameObject.SetActive(false);
                gameOverCanvas.gameObject.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// Updates the given text UI with given value, by the index:
    /// 0 = Health, 1 = Damage, 2 = Fire rate, 3 = Movement speed, 4 = Missile;
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    public void SetText(int index, string value)
    {
        textsGUI[index].text = value;
    }

    /// <summary>
    /// To be used by the Restart buttons.
    /// </summary>
    public void RestartScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
