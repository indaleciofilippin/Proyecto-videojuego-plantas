using UnityEngine;

public class menu_pausa : MonoBehaviour
{   
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    [SerializeField] private GameObject[] iconosVida; // Nuevo: array con todos los íconos de vida

    [SerializeField] private GameObject[] vidasUI; // vida1, vida2, vida3

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
    }

    public void Reanudar()
    {
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
