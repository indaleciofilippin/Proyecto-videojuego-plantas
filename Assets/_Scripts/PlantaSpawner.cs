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

    public float tiempoSpawnEtapa1 = 3f;
    public float tiempoSpawnEtapa2 = 5f;

    private SpriteRenderer sr;
    private float temporizador = 0f;

    void Start()
    {
        vidaActual = vidaMaxima;
        sr = GetComponent<SpriteRenderer>();
        ActualizarSprite();
    }

    void Update()
    {
        float porcentajeVida = (float)vidaActual / vidaMaxima;
        temporizador += Time.deltaTime;

        if (porcentajeVida > 0.6f)
        {
            if (temporizador >= tiempoSpawnEtapa1)
            {
                SpawnEnemigo();
                temporizador = 0f;
            }
        }
        else if (porcentajeVida > 0.3f)
        {
            if (temporizador >= tiempoSpawnEtapa2)
            {
                SpawnEnemigo();
                temporizador = 0f;
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

    public bool EstaEnFase3()
    {
        return ((float)vidaActual / vidaMaxima) <= 0.3f;
    }

}
