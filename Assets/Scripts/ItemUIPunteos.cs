using System;
using TMPro;
using UnityEngine;

public class ItemUIPunteos : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nombreText;
    [SerializeField] private TextMeshProUGUI punteoText;
    [SerializeField] private TextMeshProUGUI fechaText;

    public void Configurar(string nombre, int punteo, string fecha)
    {
        nombreText.text = nombre;
        punteoText.text = punteo.ToString();
        fechaText.text = DateTime.Parse(fecha).ToString("dd/MM/yyyy HH:mm");
    }
}