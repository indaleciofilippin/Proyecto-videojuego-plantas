
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManger : MonoBehaviour
{
    public PlantaSpawner spawner1;
    public PlantaSpawner spawner2;
    public PlantaSpawner spawner3;

    public GameObject menuVictoria;

    private bool victoriaMostrada = false;

    void Update()
    {
        if (!victoriaMostrada &&
            spawner1.EstaEnFase3() &&
            spawner2.EstaEnFase3() &&
            spawner3.EstaEnFase3())
        {
            menuVictoria.SetActive(true);
            victoriaMostrada = true;
            Time.timeScale = 0f; // Pausa el juego al mostrar el menú de victoria
        }
        else if (victoriaMostrada &&
            !spawner1.EstaEnFase3() &&
            !spawner2.EstaEnFase3() &&
            !spawner3.EstaEnFase3())
        {
            menuVictoria.SetActive(false);
            victoriaMostrada = false;
            Time.timeScale = 1f; // Reanuda el juego al ocultar el menú de victoria
        }
        {
            
        }
    }

}


