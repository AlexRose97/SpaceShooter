using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Configuracion")] [SerializeField]
    private int nivel = 1; // 1 = fácil, 2 = medio, 3 = difícil

    [SerializeField] private int vidaMaxima = 1;
    [SerializeField] private float velocidad;

    [Header("Disparo")] [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] spawnsPoints;
    [SerializeField] private float minTimeBullet = 1f;
    [SerializeField] private float maxTimeBullet = 2f;

    [Header("Sonidos")] [SerializeField] private AudioClip audioBullet;
    [SerializeField] private AudioClip audioDestroy;

    [Header("Animaciones")] [SerializeField]
    private GameObject explosionPrefab;

    public int Nivel => nivel; //Exponer el nivel actual

    private int vidaActual;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vidaActual = vidaMaxima;
        StartCoroutine(SpawnBullet());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0).normalized * (velocidad * Time.deltaTime));
    }


    IEnumerator SpawnBullet()
    {
        while (true)
        {
            Transform[] puntosMezclados = spawnsPoints.OrderBy(x => Random.value).ToArray();
            foreach (Transform punto in puntosMezclados)
            {
                Instantiate(bulletPrefab, punto.position, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(0f, 0.15f)); //Retrazo entre disparo
            }

            ReproduceSound(audioBullet);
            float tiempoEspera = Random.Range(minTimeBullet, maxTimeBullet);
            yield return new WaitForSeconds(tiempoEspera);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GameObject().CompareTag("BulletPlayer"))
        {
            Player player = FindFirstObjectByType<Player>();
            if (player != null)
            {
                player.AddPoints(this); //Avisarle al player que gano puntos
            }

            GameObject explosionEnemy =
                Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            ReproduceSound(audioDestroy); //Reproduce el sonido
            Destroy(other.gameObject); //destruir el laser
            Destroy(gameObject); //destruir al enemigo padre del script
            Destroy(explosionEnemy, 0.18f); //destruir al enemigo padre del script
        }
    }

    void ReproduceSound(AudioClip clip)
    {
        GameObject tempAudio = new GameObject("TempAudio");
        tempAudio.transform.position = transform.position;

        AudioSource tempSource = tempAudio.AddComponent<AudioSource>();
        tempSource.clip = clip;
        tempSource.volume = 0.6f; // 🔊 volumen deseado
        tempSource.pitch = 1.0f;
        tempSource.spatialBlend = 0f; // 0 = 2D, 1 = 3D

        tempSource.Play();
        Destroy(tempAudio, clip.length); // eliminar al terminar
    }
}