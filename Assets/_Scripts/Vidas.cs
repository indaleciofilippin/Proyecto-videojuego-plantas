using UnityEngine;
using System.Collections;

public class Vidas : MonoBehaviour
{
    [Header("Sistema de Vidas")]
    int vidas = 3;

    [Header("UI Elements")]
    public GameObject[] corazonesUI;
    public GameObject[] vidasUI;
    public GameObject menuGameOver;
    public GameObject botonPausa;

    [Header("Referencias")]
    public GameObject playerSpriteObject; // Arrastra aquí el PlayerSprite desde el inspector

    [Header("Materiales")]
    public Material materialGlow; // Material con el shader GlowSprite
    private SpriteRenderer sr;

    [Header("Configuración del Glow")]
    [SerializeField] private float transitionDuration = 0.5f;
    [SerializeField] private float maxGlowStrength = 1.5f;
    private Coroutine glowTransitionCoroutine;

    void Start()
    {
        // Obtener el SpriteRenderer del GameObject asignado
        if (playerSpriteObject != null)
        {
            sr = playerSpriteObject.GetComponent<SpriteRenderer>();
        }
        else
        {
            // Buscar automáticamente como respaldo
            sr = GetComponentInChildren<SpriteRenderer>();
            Debug.LogWarning("PlayerSpriteObject no está asignado. Buscando SpriteRenderer automáticamente...");
        }

        if (sr == null)
        {
            Debug.LogError("No se encontró SpriteRenderer. Asegúrate de asignar el PlayerSprite en el inspector o que tenga un SpriteRenderer.");
        }
        else
        {
            // Asignar el material con glow desde el inicio
            if (materialGlow != null)
            {
                sr.material = materialGlow;
            }
        }

        ActualizarUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (vidas > 0)
            {
                vidas--;
                ActualizarUI();

                // Efecto visual de daño
                StartCoroutine(DamageFlash());

                if (vidas == 0)
                {
                    Time.timeScale = 0;

                    if (menuGameOver != null)
                        menuGameOver.SetActive(true);

                    if (botonPausa != null)
                        botonPausa.SetActive(false);
                }
            }
        }
    }

    public void ActualizarUI()
    {
        // Corazones simples
        for (int i = 0; i < corazonesUI.Length; i++)
        {
            corazonesUI[i].SetActive(i < vidas);
        }

        // Imágenes animadas
        for (int i = 0; i < vidasUI.Length; i++)
        {
            vidasUI[i].SetActive(false);
        }

        if (vidas > 0 && vidas <= vidasUI.Length)
        {
            GameObject vidaActiva = vidasUI[vidas - 1];
            vidaActiva.SetActive(true);

            ImageSwapper2 swapper = vidaActiva.GetComponent<ImageSwapper2>();
            if (swapper != null)
            {
                swapper.RestartAnimation();
            }
        }

        // Cambiar material con transición suave
        ActualizarMaterial();
    }

    private void ActualizarMaterial()
    {
        if (sr == null || materialGlow == null) return;

        // Detener transición anterior si existe
        if (glowTransitionCoroutine != null)
        {
            StopCoroutine(glowTransitionCoroutine);
        }

        if (vidas == 3)
        {
            // Activar glow gradualmente
            glowTransitionCoroutine = StartCoroutine(TransitionGlow(0f, maxGlowStrength));
        }
        else
        {
            // Desactivar glow gradualmente
            glowTransitionCoroutine = StartCoroutine(TransitionGlow(maxGlowStrength, 0f));
        }
    }

    private IEnumerator TransitionGlow(float startStrength, float targetStrength)
    {
        if (!materialGlow.HasProperty("_GlowStrength")) yield break;

        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float currentStrength = Mathf.Lerp(startStrength, targetStrength, elapsed / transitionDuration);
            materialGlow.SetFloat("_GlowStrength", currentStrength);
            yield return null;
        }

        materialGlow.SetFloat("_GlowStrength", targetStrength);
    }

    private IEnumerator TransitionToNormal()
    {
        // Ya no necesitamos este método
        yield break;
    }

    private IEnumerator DamageFlash()
    {
        // Efecto de flash rojo al recibir daño
        Color originalColor = sr.color;
        sr.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        sr.color = originalColor;
    }

    public int GetVidas()
    {
        return vidas;
    }

    // Método para recuperar vida (por si tienes power-ups)
    public void RecuperarVida()
    {
        if (vidas < 3)
        {
            vidas++;
            ActualizarUI();
        }
    }

    // Método para configurar el glow manualmente
    public void SetGlowIntensity(float intensity)
    {
        if (materialGlow != null && materialGlow.HasProperty("_GlowStrength"))
        {
            materialGlow.SetFloat("_GlowStrength", intensity);
        }
    }

    void OnDestroy()
    {
        // Limpiar el glow al destruir el objeto
        if (materialGlow != null && materialGlow.HasProperty("_GlowStrength"))
        {
            materialGlow.SetFloat("_GlowStrength", 0f);
        }
    }
}