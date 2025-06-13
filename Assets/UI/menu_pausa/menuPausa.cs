using UnityEngine;

public class menu_pausa : MonoBehaviour
{   
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    [SerializeField] private GameObject[] iconosVida; // Nuevo: array con todos los íconos de vida

    [SerializeField] private GameObject[] vidasUI; // vida1, vida2, vida3

    private AudioSource musicaGameplay; // Referencia a la música del gameplay

    public void Pausa()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);

        // Ocultar todos los íconos de vida
        foreach (GameObject icono in iconosVida)
        {
            if (icono != null)
                icono.SetActive(false);
        }

        menuPausa.SetActive(true);

        // Ocultar todas las vidas animadas
        foreach (GameObject vida in vidasUI)
        {
            if (vida != null)
                vida.SetActive(false);
        }

        // Obtenemos la referencia a la música del gameplay en la Main Camera
        musicaGameplay = Camera.main?.GetComponent<AudioSource>();
        if (musicaGameplay == null)
        {
            Debug.LogWarning("No se encontró AudioSource en la Main Camera");
        }

        // Detener la música del gameplay
        if (musicaGameplay != null && musicaGameplay.isPlaying)
        {
            musicaGameplay.Pause(); // Usamos Pause en lugar de Stop para poder reanudarla después
        }
    }

    public void Reanudar()
    {

        // Reanudar la música del gameplay
        if (musicaGameplay != null && !musicaGameplay.isPlaying)
        {
            musicaGameplay.Play();
        }

        Time.timeScale = 1f;
        botonPausa.SetActive(true);

        // Mostrar todos los íconos de vida
        foreach (GameObject icono in iconosVida)
        {
            if (icono != null)
                icono.SetActive(true);
        }

        menuPausa.SetActive(false);

        // Mostrar vida animada actual
        Vidas sistemaVidas = Object.FindFirstObjectByType<Vidas>();
        if (sistemaVidas != null)
        {
            sistemaVidas.ActualizarUI();
        }
    }
}
