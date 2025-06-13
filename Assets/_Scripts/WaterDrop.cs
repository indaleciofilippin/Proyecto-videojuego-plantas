using UnityEngine;
using System.Collections;

public class WaterDrop : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public int damage;
    public float fadeOutDuration = 1f; // Duración del efecto de desvanecimiento
    public float gravityScale = 0.5f; // Fuerza de gravedad para la caída del agua
    public float caidaDelayTime = 0.3f; // Tiempo antes de que comience a caer

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No se encontró SpriteRenderer en la gota de agua");
        }
        
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Inicialmente sin gravedad
            rb.gravityScale = 0f;
            StartCoroutine(AplicarGravedadDespuesDeTiempo());
        }
        
        // Iniciamos la corrutina de desvanecimiento
        StartCoroutine(FadeOutAndDestroy());
    }

    public void Launch(Vector2 direction)
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyFunctions enemy = collision.GetComponent<EnemyFunctions>();
        if (collision.TryGetComponent<PlantaSpawner>(out var planta))
        {
            planta.CurarPlanta(damage);
        }
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    private IEnumerator AplicarGravedadDespuesDeTiempo()
    {
        // Esperamos un tiempo antes de aplicar la gravedad
        yield return new WaitForSeconds(caidaDelayTime);
        
        // Aplicamos la gravedad para que comience a caer
        rb.gravityScale = gravityScale;
    }

    private IEnumerator FadeOutAndDestroy()
    {
        // Esperamos hasta que quede el tiempo de desvanecimiento
        float waitTime = lifetime - fadeOutDuration;
        if (waitTime > 0)
        {
            yield return new WaitForSeconds(waitTime);
        }

        // Comenzamos el efecto de desvanecimiento
        float elapsedTime = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Destruimos el objeto cuando se completa la animación
        Destroy(gameObject);
    }
}
