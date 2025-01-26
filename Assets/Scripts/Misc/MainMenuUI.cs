using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void StartButton(){
        Debug.Log("start");
    }

    public void AboutButton(){
        Debug.Log("about");
    }

    public void ExitButton(){
        Application.Quit();
    }
}
