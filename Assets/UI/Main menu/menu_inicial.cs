using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_inicial : MonoBehaviour
{
    //[SerializeField] private GameObject canvasMenu;
    //[SerializeField] private GameObject cameraMenu;
    //[SerializeField] private GameObject menuMusica;

    /*void Start()
    {
        Time.timeScale = 0f; // Pausa el juego al iniciar
        canvasMenu.SetActive(true);
    }*/

    public void Jugar(string name)
    {
        //canvasMenu.SetActive(false);
        //cameraMenu.SetActive(false);
        //Time.timeScale = 1f; // Reanuda el juego
        //menuMusica.SetActive(true);
        SceneManager.LoadScene(name); // Carga la escena del juego
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}

