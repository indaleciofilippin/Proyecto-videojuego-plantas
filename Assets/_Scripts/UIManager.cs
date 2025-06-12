using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
 public void RestartScene(){

        Time.timeScale = 1; // Reinicia el tiempo
        SceneManager.LoadScene(0);

    }
}
