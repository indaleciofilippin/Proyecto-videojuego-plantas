using UnityEngine;

public class PlantaSpawner : MonoBehaviour
{
    public int vidaMaxima = 500;
    private int vidaActual;

    public GameObject enemigoPrefab;
    public Transform puntoSpawn;

    public Sprite spriteEtapa1; // Marchita: 60-100%
    public Sprite spriteEtapa2; // Semi-marchita: 30-60%
    public Sprite spriteEtapa3; // Florecida: 0-30%

    public Vector2 rangoSpawnEtapa1 = new(0.8f, 2.0f);  // spawn más frecuente
    public Vector2 rangoSpawnEtapa2 = new(1.5f, 3.5f);  // spawn más lento

    private SpriteRenderer sr;
    private float temporizador = 0f;
    private float tiempoSiguienteSpawn = 0f;

    void Start()
    {
        vidaActual = vidaMaxima;
        sr = GetComponent<SpriteRenderer>();
        ActualizarSprite();
        tiempoSiguienteSpawn = CalcularTiempoSpawn(); // primer tiempo aleatorio
    }

    void Update()
    {
        float porcentajeVida = (float)vidaActual / vidaMaxima;
        temporizador += Time.deltaTime;

        // Solo hacer spawn si no está en la etapa 3 (florecida)
        if (porcentajeVida > 0.3f)
        {
            if (temporizador >= tiempoSiguienteSpawn)
            {
                SpawnEnemigo();
                temporizador = 0f;
                tiempoSiguienteSpawn = CalcularTiempoSpawn(); // calcular próximo intervalo aleatorio
            }
        }

        ActualizarSprite();
    }

    void ActualizarSprite()
    {
        float porcentajeVida = (float)vidaActual / vidaMaxima;

        if (porcentajeVida > 0.6f)
            sr.sprite = spriteEtapa1;
        else if (porcentajeVida > 0.3f)
            sr.sprite = spriteEtapa2;
        else
            sr.sprite = spriteEtapa3;
    }

    public void CurarPlanta(int cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
    }

    void SpawnEnemigo()
    {
        Instantiate(enemigoPrefab, puntoSpawn.position, Quaternion.identity);
    }

    float CalcularTiempoSpawn()
    {
        float porcentajeVida = (float)vidaActual / vidaMaxima;

        if (porcentajeVida > 0.6f)
            return Random.Range(rangoSpawnEtapa1.x, rangoSpawnEtapa1.y);
        else
            return Random.Range(rangoSpawnEtapa2.x, rangoSpawnEtapa2.y);
    }

    public bool EstaEnFase3()
    {
        return ((float)vidaActual / vidaMaxima) <= 0.3f;
    }
}
