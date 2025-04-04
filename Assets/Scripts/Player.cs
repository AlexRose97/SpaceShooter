using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float ratioBullet;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject spawnPoint1;
    [SerializeField] private GameObject spawnPoint2;

    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        DelimintarMovimiento();
        Disparar();
    }

    void Movimiento()
    {
        /*** Agregar movimiento en x,y ***/
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(horizontal, vertical).normalized * moveSpeed * Time.deltaTime);
    }

    void DelimintarMovimiento()
    {
        /*** Delimitar movimiento en la pantalla ***/
        float xClamp = Mathf.Clamp(transform.position.x, -8f, 8f);
        float yClamp = Mathf.Clamp(transform.position.y, -4.4f, 4.4f);
        transform.position = new Vector3(xClamp, yClamp, transform.position.z);
    }

    void Disparar()
    {
        timer += 1 * Time.deltaTime; //contador de tiempo
        /*se realiza un disparo cuando se preciona la tecla "espacio solo si ya se cumplio el tiempo minimo"*/
        if (Input.GetKeyDown(KeyCode.Space) && timer > ratioBullet)
        {
            Instantiate(bulletPrefab, spawnPoint1.transform.position, Quaternion.identity);
            Instantiate(bulletPrefab, spawnPoint2.transform.position, Quaternion.identity);
            timer = 0; //reiniciar el contador de tiempo
        }
    }
}