using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private TextMeshProUGUI textOleada;
    [SerializeField] private Image imageBorder;

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
        //Niveles
        for (int i = 0; i < 3; i++)
        {
            //Oleadas
            for (int j = 0; j < 3; j++)
            {
                imageBorder.enabled = true;
                textOleada.text = $"Nivel {i + 1} - Oleada {j + 1}";
                yield return new WaitForSeconds(2f); //Tiempo para borrar el texto
                imageBorder.enabled = false;
                textOleada.text = "";
                //Enemigos
                for (int k = 0; k < 5; k++)
                {
                    Vector3 randomPosition =
                        new Vector3(transform.position.x, Random.Range(-3.2f, 3.2f), transform.position.z);
                    Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
                    yield return new WaitForSeconds(1f); //Tiempo entre cada enemigo
                }

                yield return new WaitForSeconds(3f); //Tiempo entre cada Oleada
            }

            yield return new WaitForSeconds(5f); //Tiempo entre cada Nivel
        }
    }
}