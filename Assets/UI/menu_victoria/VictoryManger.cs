using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManger : MonoBehaviour
{
    public PlantaSpawner spawner1;
    public PlantaSpawner spawner2;
    public PlantaSpawner spawner3;

    public GameObject menuVictoria;
    public AudioClip musicaVictoria;
    public float volumenMusica = 1.0f;
    
    private bool victoriaMostrada = false;
    private AudioSource fuenteAudio;
    private AudioSource musicaGameplay; // Referencia a la música del gameplay

    void Start()
    {
        // Configuración del AudioSource para música de victoria
        fuenteAudio = GetComponent<AudioSource>();
        if (fuenteAudio == null)
        {
            fuenteAudio = gameObject.AddComponent<AudioSource>();
        }
        
        // Configuración de la canción
        fuenteAudio.playOnAwake = false;
        fuenteAudio.loop = true;
        fuenteAudio.volume = volumenMusica;
        fuenteAudio.clip = musicaVictoria;

        // Obtenemos la referencia a la música del gameplay en la Main Camera
        musicaGameplay = Camera.main?.GetComponent<AudioSource>();
        if (musicaGameplay == null)
        {
            Debug.LogWarning("No se encontró AudioSource en la Main Camera");
        }
    }

    void Update()
    {
        if (!victoriaMostrada &&
            spawner1.EstaEnFase3() &&
            spawner2.EstaEnFase3() &&
            spawner3.EstaEnFase3())
        {
            menuVictoria.SetActive(true);
            victoriaMostrada = true;
            Time.timeScale = 0f;
            
            // Detener la música del gameplay
            if (musicaGameplay != null && musicaGameplay.isPlaying)
            {
                musicaGameplay.Pause(); // Usamos Pause en lugar de Stop para poder reanudarla después
            }
            
            // Reproducir la canción de victoria
            if (musicaVictoria != null && !fuenteAudio.isPlaying)
            {
                fuenteAudio.Play();
            }
        }
        else if (victoriaMostrada &&
            !spawner1.EstaEnFase3() &&
            !spawner2.EstaEnFase3() &&
            !spawner3.EstaEnFase3())
        {
            menuVictoria.SetActive(false);
            victoriaMostrada = false;
            Time.timeScale = 1f;
            
            // Detener la canción de victoria
            fuenteAudio.Stop();
            
            // Reanudar la música del gameplay
            if (musicaGameplay != null && !musicaGameplay.isPlaying)
            {
                musicaGameplay.Play();
            }
        }
    }
}


