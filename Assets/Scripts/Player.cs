using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed; //velocidad de movimiento
    [SerializeField] private float ratioBullet; //frecuencia de los disparos
    [SerializeField] private GameObject bulletPrefab; //UI disparo
    [SerializeField] private GameObject spawnPoint1; //posicion bala1
    [SerializeField] private GameObject spawnPoint2; //posicion bala2


    [SerializeField] private TextMeshProUGUI textLife;
    [SerializeField] private TextMeshProUGUI textPoints;
    [SerializeField] private Slider sliderLife;
    [SerializeField] private Transform livesContainer;
    [SerializeField] private GameObject gameOverContainer;
    [SerializeField] private GameObject stopGameContainer;

    [Header("Sonidos")] [SerializeField] private AudioClip audioBullet;
    [SerializeField] private AudioClip audioImpact;
    [SerializeField] private AudioClip audioDestroy;

    [Header("Animaciones")] [SerializeField]
    private GameObject explosionPlayerPrefab;

    private int _totalLives = 3;
    private int _maxLives = 3;
    private float _healthPerLife = 100f;
    private float _currentHealth;
    private int _score;
    private float _timer;

    public int Score => _score;
    public int TotalLives => _totalLives;
    public float CurrentHealth => _currentHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialValues();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        DelimintarMovimiento();
        Disparar();
        MenuPausa();
    }

    void MenuPausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (stopGameContainer.activeSelf)
            {
                stopGameContainer.SetActive(false);
                Time.timeScale = 1f; // continuar el juego
            }
            else
            {
                stopGameContainer.SetActive(true);
                Time.timeScale = 0f; // detener el juego
            }
        }
    }

    void Movimiento()
    {
        /*** Agregar movimiento en x,y ***/
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(horizontal, vertical).normalized * (moveSpeed * Time.deltaTime));
    }

    void DelimintarMovimiento()
    {
        /*** Delimitar movimiento en la pantalla ***/
        float xClamp = Mathf.Clamp(transform.position.x, -8f, 8f);
        float yClamp = Mathf.Clamp(transform.position.y, -3.2f, 3.2f);
        transform.position = new Vector3(xClamp, yClamp, transform.position.z);
    }

    void Disparar()
    {
        _timer += 1 * Time.deltaTime; //contador de tiempo
        /*se realiza un disparo cuando se preciona la tecla "espacio solo si ya se cumplio el tiempo minimo"*/
        if (Input.GetKeyDown(KeyCode.Space) && _timer > ratioBullet)
        {
            Instantiate(bulletPrefab, spawnPoint1.transform.position, Quaternion.identity);
            Instantiate(bulletPrefab, spawnPoint2.transform.position, Quaternion.identity);
            ReproduceSound(audioBullet); //Reproducir sonido
            _timer = 0; //reiniciar el contador de tiempo
        }
    }

    void ReproduceSound(AudioClip clip)
    {
        GameObject tempAudio = new GameObject("TempAudio");
        tempAudio.transform.position = transform.position;

        AudioSource tempSource = tempAudio.AddComponent<AudioSource>();
        tempSource.clip = clip;
        tempSource.volume = 1.0f; // 🔊 volumen deseado
        tempSource.pitch = 1.0f;
        tempSource.spatialBlend = 0f; // 0 = 2D, 1 = 3D

        tempSource.Play();
        Destroy(tempAudio, clip.length); // eliminar al terminar
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BulletEnemy"))
        {
            float damage = DamageConstants.GetDamage(other.tag);
            TakeDamage(damage);
            Destroy(other.gameObject);
            ReproduceSound(audioImpact);
            UpdateUIValues();
        }

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                float damage = DamageConstants.GetDamage(enemy.tag, enemy.Nivel);
                TakeDamage(damage);
                AddPoints(enemy);
                Destroy(other.gameObject);
                ReproduceSound(audioImpact);
            }
        }
    }

    private void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            _totalLives--;
            AddOrRemoveHeartIcon(false);
            ReproduceSound(audioDestroy); //Reproducir sonido
            //Animar explosion al perder una vida
            GameObject explosionPlayer =
                Instantiate(explosionPlayerPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(explosionPlayer, 0.18f); //destruir prefab de explosion
            if (_totalLives <= 0)
            {
                Destroy(gameObject);
                Time.timeScale = 0f; // detener el juego
                gameOverContainer.SetActive(true);
            }
            else
            {
                _currentHealth = _healthPerLife; //Reinicia el % de vida
            }
        }
    }

    public void RecuperarVida()
    {
        // Si la vida actual está incompleta, primero la rellena
        if (_currentHealth < _healthPerLife)
        {
            _currentHealth = _healthPerLife;
        }
        else
        {
            //sino aumenta la cantidad de vidas
            _totalLives++;
            _totalLives = Mathf.Clamp(_totalLives, 0, _maxLives);
            AddOrRemoveHeartIcon(true);
        }

        UpdateUIValues(); // Actualiza corazones o barra de vida
    }

    /// <summary>
    /// Permite ocultar/mostrar los 3 iconos de corazon
    /// </summary>
    /// <param name="add">true=mostrar; false=ocultar</param>
    /// <param name="updateAll">true = actualizar todos los corazones, false=actualiza solo 1 corazon</param>
    void AddOrRemoveHeartIcon(bool add, bool updateAll = false)
    {
        for (int i = livesContainer.childCount - 1; i >= 0; i--)
        {
            Transform heartContainer = livesContainer.GetChild(i);
            Image heartImage = heartContainer.GetComponentInChildren<Image>();

            if (heartImage != null && heartImage.enabled != add)
            {
                heartImage.enabled = add;
                if (!updateAll)
                {
                    break;
                }
            }
        }
    }

    private void UpdateUIValues()
    {
        textPoints.text = $"PUNTOS: {_score}";
        textLife.text = $"VIDA: {_currentHealth}%";
        sliderLife.value = _currentHealth / _healthPerLife;
    }

    private void InitialValues()
    {
        AddOrRemoveHeartIcon(true, true); //reiniciar las vidas graficamente
        _totalLives = 3;
        _healthPerLife = 100f;
        _currentHealth = _healthPerLife;
        _score = 0;
        UpdateUIValues();
    }

    /// <summary>
    /// Incrementa la puntuación del jugador según el tipo de enemigo destruido,
    /// utiliza un tag y actualiza la interfaz de usuario.
    /// </summary>
    /// <param name="enemy">Instancia del enemigo, usada para determinar los puntos</param>
    public void AddPoints(Enemy enemy)
    {
        _score += DamageConstants.GetPoints(enemy.tag, enemy.Nivel);
        UpdateUIValues();
    }

    public void ActivarItem(TipoItem tipo)
    {
        switch (tipo)
        {
            case TipoItem.Vida:
                RecuperarVida();
                break;
            case TipoItem.Escudo:
                break;
            case TipoItem.Misil:
                break;
            case TipoItem.Radar:
                break;
        }
    }
}