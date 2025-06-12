using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIDestinationSetter))]
public class AssignPlayerAsTarget : MonoBehaviour
{
    void Start()
    {
        // Busca la instancia del jugador en la escena
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO == null)
        {
            Debug.LogError("No se encontró ningún GameObject con tag 'Player'");
            return;
        }
        // Asigna correctamente su transform
        GetComponent<AIDestinationSetter>().target = playerGO.transform;
    }
}
