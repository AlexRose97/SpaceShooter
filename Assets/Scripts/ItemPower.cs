using UnityEngine;

public enum TipoItem
{
    Vida,
    Escudo,
    Misil,
    Radar
}

public class ItemPower : MonoBehaviour
{
    [Header("Movimiento")] [SerializeField]
    private float velocidad = 25;

    [SerializeField] private float minY = -3.2f;
    [SerializeField] private float maxY = 3.2f;


    public TipoItem tipoItem;
    private Vector3 _direccion;

    void Start()
    {
        _direccion = new Vector3(-1, Random.Range(-1f, 1f), 0).normalized;
    }

    void Update()
    {
        transform.Translate(_direccion * (velocidad * Time.deltaTime));

        // Si toca los límites verticales, invierte dirección Y
        if (transform.position.y <= minY || transform.position.y >= maxY)
        {
            _direccion.y = -_direccion.y;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.ActivarItem(tipoItem);
            }

            Destroy(gameObject); //eliminar el ítem
        }
    }
}