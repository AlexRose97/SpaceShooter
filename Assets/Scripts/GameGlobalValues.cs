using UnityEngine;

public class GameDifficultyData
{
    public int enemigos;
    public int oleadas;
    public float tiempoEnemigo;
    public float itemsDrop;
}

public class GameGlobalValues : MonoBehaviour
{
    public static string NombreJugador { get; set; }
    public static string Dificultad { get; set; }


    public static GameDifficultyData ObtenerDatosPorDificultad(string dificultad)
    {
        var data = new GameDifficultyData();

        switch (dificultad.ToUpperInvariant())
        {
            case "FACIL":
                data.enemigos = 5;
                data.oleadas = 1;
                data.itemsDrop = 0.25f;
                data.tiempoEnemigo = 1f;
                break;
            case "MODERADO":
                data.enemigos = 10;
                data.oleadas = 3;
                data.itemsDrop = 0.15f;
                data.tiempoEnemigo = 0.75f;
                break;
            case "DIFICIL":
                data.enemigos = 20;
                data.oleadas = 4;
                data.itemsDrop = 0.05f;
                data.tiempoEnemigo = 0.5f;
                break;
        }

        return data;
    }
}