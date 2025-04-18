using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PartidaData
{
    public string nombreJugador;
    public string dificultad;
    public int puntuacion;
    public string fecha;
}

[System.Serializable]
public class ListaDePartidas
{
    public List<PartidaData> partidas = new();
}

public class GameMananger : MonoBehaviour
{
    private string RutaArchivo => Application.persistentDataPath + "/partidas.json";
    public ListaDePartidas lista = new();

    private void Awake()
    {
        CargarDatos();
    }

    public void GuardarPartida()
    {
        Player player = FindFirstObjectByType<Player>();
        lista.partidas.Add(new PartidaData
        {
            nombreJugador = GameGlobalValues.NombreJugador,
            dificultad = GameGlobalValues.Dificultad,
            puntuacion = player.Score,
            fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        });
        string json = JsonUtility.ToJson(lista, true);
        File.WriteAllText(RutaArchivo, json);
    }

    public void CargarDatos()
    {
        if (File.Exists(RutaArchivo))
        {
            string json = File.ReadAllText(RutaArchivo);
            lista = JsonUtility.FromJson<ListaDePartidas>(json);
        }
    }

    public void FinJuego()
    {
        GuardarPartida();
        SceneManager.LoadScene("MenuPrincipal");
        Time.timeScale = 1f; // detener el juego
    }

    public void Continuar()
    {
        Debug.Log(RutaArchivo);
        Time.timeScale = 1f; // detener el juego
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("Juego");
        Time.timeScale = 1f; // detener el juego
    }
}