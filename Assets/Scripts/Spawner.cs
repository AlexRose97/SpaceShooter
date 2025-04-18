using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Spawner : MonoBehaviour
{
    [Header("Enemigos")] [SerializeField] private GameObject enemyPrefab1;
    [SerializeField] private GameObject enemyPrefab2;
    [SerializeField] private GameObject enemyPrefab3;
    [SerializeField] private TextMeshProUGUI textOleada;
    [SerializeField] private Image imageBorder;
    [SerializeField] private GameObject gameOverContainer;

    [Header("ItemsPowers")] [SerializeField]
    private GameObject[] itemPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -10f || transform.position.x > 10f ||
            transform.position.y < -6f || transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnEnemies()
    {
        var datos = GameGlobalValues.ObtenerDatosPorDificultad(GameGlobalValues.Dificultad);
        //Niveles
        for (int i = 0; i < 3; i++)
        {
            //Oleadas
            for (int j = 0; j < datos.oleadas; j++)
            {
                imageBorder.enabled = true;
                textOleada.text = $"Nivel {i + 1} - Oleada {j + 1}";
                yield return new WaitForSeconds(1f); //Tiempo para borrar el texto
                imageBorder.enabled = false;
                textOleada.text = "";
                //Enemigos
                for (int k = 0; k < datos.enemigos; k++)
                {
                    Vector3 randomPosition =
                        new Vector3(transform.position.x, Random.Range(-3.2f, 3.2f), transform.position.z);
                    switch (i)
                    {
                        case 0:
                            Instantiate(enemyPrefab1, randomPosition, Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(enemyPrefab2, randomPosition, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(enemyPrefab3, randomPosition, Quaternion.identity);
                            break;
                    }

                    // 💡 Aparición aleatoria de powers con itemsDrop% de probabilidad
                    if (Random.value < datos.itemsDrop)
                    {
                        int itemIndex = Random.Range(0, itemPrefabs.Length);
                        Vector3 itemPos = new Vector3(transform.position.x, Random.Range(-3.2f, 3.2f),
                            transform.position.z);
                        Instantiate(itemPrefabs[itemIndex], itemPos, Quaternion.identity);
                    }

                    yield return new WaitForSeconds(datos.tiempoEnemigo); //Tiempo entre cada enemigo
                }

                yield return new WaitForSeconds(3f); //Tiempo entre cada Oleada
            }

            yield return new WaitForSeconds(5f); //Tiempo entre cada Nivel
        }


        Time.timeScale = 0f; // detener el juego
        gameOverContainer.SetActive(true);
    }
}