using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;

    /// <summary>
    /// Adds score and updates the text ui.
    /// </summary>
    /// <param name="addition"></param>
    public void AddScore(int addition)
    {
        score+=addition;
        scoreText.text = score.ToString();
    }
}
