using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AboutButton()
    {
        Debug.Log("about");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
