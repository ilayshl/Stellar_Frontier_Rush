using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Responsible for the Main Menu UI and buttons functionality.
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Animator blackScreen;
    private bool isTransitioning;

    public void StartButton()
    {
        if (!isTransitioning)
        {
            blackScreen.SetTrigger("startButton");
            Invoke("StartGame", 2);
            isTransitioning = !isTransitioning;
        }
    }

    public void AboutButton()
    {
        Debug.Log("about");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
