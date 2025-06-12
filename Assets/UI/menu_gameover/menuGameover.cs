using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarJuego : MonoBehaviour
{
    public void Reiniciar()
    {
        Time.timeScale = 1f; // Por si el juego está en pausa
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
