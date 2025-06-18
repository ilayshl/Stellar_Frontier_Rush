using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
