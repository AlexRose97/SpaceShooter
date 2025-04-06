using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMananger : MonoBehaviour
{
    public void FinJuego()
    {
        SceneManager.LoadScene("MenuPrincipal");
        Time.timeScale = 1f; // detener el juego
    }
    
    public void Continuar()
    {
        Time.timeScale = 1f; // detener el juego
    }
    
    public void Reiniciar()
    {
        SceneManager.LoadScene("Juego");
        Time.timeScale = 1f; // detener el juego
    }
}