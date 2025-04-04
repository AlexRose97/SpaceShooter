using UnityEngine;

public class Disparo : MonoBehaviour
{
    [SerializeField] private float velocidad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0).normalized * velocidad * Time.deltaTime);
    }
}