using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*** Agregar movimiento en x,y ***/
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(horizontal, vertical).normalized * moveSpeed * Time.deltaTime);

        /*** Delimitar movimiento en la pantalla ***/
        float xClamp = Mathf.Clamp(transform.position.x, -8f, 8f);
        float yClamp = Mathf.Clamp(transform.position.y, -4.4f, 4.4f);
        transform.position = new Vector3(xClamp, yClamp, transform.position.z);
    }
}