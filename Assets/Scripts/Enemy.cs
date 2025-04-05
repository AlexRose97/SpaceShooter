using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject spawnPoint;


    [SerializeField] private AudioClip audioBullet;
    [SerializeField] private AudioClip audioDestroy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            Instantiate(bulletPrefab, spawnPoint.transform.position, Quaternion.identity);
            ReproduceSound(audioBullet); //Reproduce el sonido
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GameObject().CompareTag("BulletPlayer"))
        {
            Player player = FindFirstObjectByType<Player>();
            if (player != null)
            {
                player.AddPoints(gameObject.tag); //Avisarle al player que gano puntos
            }

            ReproduceSound(audioDestroy); //Reproduce el sonido
            Destroy(other.gameObject); //destruir el laser
            Destroy(gameObject); //destruir al enemigo padre del script
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