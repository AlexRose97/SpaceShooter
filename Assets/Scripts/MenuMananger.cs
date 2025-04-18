using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMananger : MonoBehaviour
{
    [Header("Paneles")] [SerializeField] private GameObject panelInformacion;
    [SerializeField] private GameObject panelMenuPrincipal;
    [SerializeField] private GameObject panelPunteos;

    [Header("SubPaneles")] [SerializeField]
    private GameObject panelObjetivo;

    [SerializeField] private GameObject panelEnemigos;
    [SerializeField] private GameObject panelPowerUps;

    [Header("Botones")] [SerializeField] private Button btnObjetivo;
    [SerializeField] private Button btnEnemigos;
    [SerializeField] private Button btnPowerUps;

    [Header("Colores")] [SerializeField] private Color colorActivo = new Color(0.2f, 0.6f, 1f); // Azul claro
    [SerializeField] private Color colorInactivo = Color.white;

    [Header("UI-Prefabs")] [SerializeField]
    private GameObject itemUIPrefab;

    [SerializeField] private Transform contenedorItems;
    private string RutaArchivo => Application.persistentDataPath + "/partidas.json";
    public ListaDePartidas lista = new();

    private void Awake()
    {
        CargarDatos();
    }

    /// <summary>
    /// Funcion para cargar punteos guardados
    /// </summary>
    public void CargarDatos()
    {
        if (File.Exists(RutaArchivo))
        {
            string json = File.ReadAllText(RutaArchivo);
            lista = JsonUtility.FromJson<ListaDePartidas>(json);
        }
    }


    private void Start()
    {
        panelMenuPrincipal.SetActive(true);
        panelInformacion.SetActive(false);
        panelPunteos.SetActive(false);
    }

    /// <summary>
    /// Funcion para iniciarl el juego
    /// </summary>
    public void ButtonPlay()
    {
        SceneManager.LoadScene("Juego");
    }

    /// <summary>
    /// Funcion para cerrar el juego
    /// </summary>
    public void ButtonExit()
    {
        Debug.Log("Exit Application");
        Application.Quit();
    }


    /// <summary>
    /// Funcion para activar los paneles principales de la ventana informacion
    /// </summary>
    /// <param name="panel">Nombre del panel ej: Informacion</param>
    public void ActivarPanel(string panel)
    {
        // Activar el panel correspondiente
        if (panel == "Informacion")
        {
            panelMenuPrincipal.SetActive(false);
            panelInformacion.SetActive(true);
            panelPunteos.SetActive(false);
            //seleccionar el primer subpanel por defecto
            panelObjetivo.SetActive(true);
            btnObjetivo.image.color = colorActivo;
            //inactivar el resto de subpaneles
            panelEnemigos.SetActive(false);
            panelPowerUps.SetActive(false);
            btnEnemigos.image.color = colorInactivo;
            btnPowerUps.image.color = colorInactivo;
        }
        else if (panel == "MenuPrincipal")
        {
            panelMenuPrincipal.SetActive(true);
            panelInformacion.SetActive(false);
            panelPunteos.SetActive(false);
        }
        else if (panel == "Punteos")
        {
            panelMenuPrincipal.SetActive(false);
            panelInformacion.SetActive(false);
            panelPunteos.SetActive(true);
            MostrarRanking();
        }
    }

    /// <summary>
    /// Funcion para activar los subpaneles de la ventana informacion
    /// </summary>
    /// <param name="panel">Nombre del subpanel ej: Objetivo</param>
    public void ActivarSubPanelInformacion(string panel)
    {
        // Activar el sub_panel correspondiente
        panelObjetivo.SetActive(panel == "Objetivo");
        panelEnemigos.SetActive(panel == "Enemigos");
        panelPowerUps.SetActive(panel == "Poderes");

        // Cambiar colores de los botones
        btnObjetivo.image.color = (panel == "Objetivo") ? colorActivo : colorInactivo;
        btnEnemigos.image.color = (panel == "Enemigos") ? colorActivo : colorInactivo;
        btnPowerUps.image.color = (panel == "Poderes") ? colorActivo : colorInactivo;
    }

    public void MostrarRanking()
    {
        // Limpia el contenido anterior
        foreach (Transform hijo in contenedorItems)
        {
            Destroy(hijo.gameObject);
        }

        // Ordenar por puntuación descendente
        var partidasOrdenadas = lista.partidas
            .OrderByDescending(p => p.puntuacion)
            .ToList();

        foreach (var partida in partidasOrdenadas)
        {
            GameObject item = Instantiate(itemUIPrefab, contenedorItems);
            var ui = item.GetComponent<ItemUIPunteos>();
            ui.Configurar(partida.nombreJugador, partida.puntuacion, partida.fecha);
        }
    }
}