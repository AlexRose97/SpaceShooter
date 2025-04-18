using System;
using TMPro;
using UnityEngine;

public class ItemUIPunteos : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nombreText;
    [SerializeField] private TextMeshProUGUI dificultadText;
    [SerializeField] private TextMeshProUGUI punteoText;
    [SerializeField] private TextMeshProUGUI fechaText;

    public void Configurar(string nombre, int punteo, string fecha, string dificultad)
    {
        nombreText.text = nombre;
        dificultadText.text = dificultad;
        punteoText.text = punteo.ToString("N0");
        fechaText.text = DateTime.Parse(fecha).ToString("dd/MM/yyyy HH:mm");
    }
}