using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnBullet());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0).normalized * velocidad * Time.deltaTime);
    }


    IEnumerator SpawnBullet()
    {
        while (true)
        {
            Instantiate(bulletPrefab, spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GameObject().CompareTag("BulletPlayer"))
        {
            Destroy(other.gameObject);//destruir el laser
            Destroy(gameObject);//destruir al enemigo padre del script
        }
    }
}