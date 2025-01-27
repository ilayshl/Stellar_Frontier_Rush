using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Animator blackScreen;

    public void StartButton()
    {
        blackScreen.SetTrigger("startButton");
        Invoke("StartGame", 2);
    }

    public void AboutButton()
    {
        Debug.Log("about");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void StartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
