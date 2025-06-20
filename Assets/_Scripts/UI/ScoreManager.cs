using TMPro;
using UnityEngine;

/// <summary>
/// Responsible for the score and everywhere it is displayed.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameoverScoreText;
    private int score;

    void Start()
    {
        GameManager.Instance.OnGameStateChanged += OnGameStateChange;
    }

    void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChange;
    }

    /// <summary>
    /// Adds score and updates the text ui.
    /// </summary>
    /// <param name="addition"></param>
    public void AddScore(int addition)
    {
        score += addition;
        scoreText.text = score.ToString();
    }

    private void OnGameStateChange(GameState state)
    {
        if (state == GameState.GameOver)
        {
            string currentText = gameoverScoreText.text;
            gameoverScoreText.SetText($"{currentText}\n{score}");
        }
    }
}
