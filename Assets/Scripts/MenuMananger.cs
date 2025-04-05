using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMananger : MonoBehaviour
{
    public void ButtonPlay()
    {
        SceneManager.LoadScene("Juego");
    }

    public void ButtonExit()
    {
        Debug.Log("Exit Application");
        Application.Quit();
    }
}