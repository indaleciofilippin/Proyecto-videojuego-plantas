using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("pato");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
