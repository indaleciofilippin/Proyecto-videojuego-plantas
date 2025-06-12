using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Referencia a la posición del jugador
    public Transform player;
    //Distancia entre la cámara y el jugador
    Vector3 distancia;
    Vector3 posActual;
    Vector3 posFinal;
    [Range(0,1)]public float velocidad;//0-1
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //vector de distacia entre la cámara y el jugador
        distancia = player.position - this.transform.position;
    }

    // Update is called once per frame
   // ...existing code...
void FixedUpdate()
{
    posActual = this.transform.position;
    // Solo la X sigue al jugador, la Y se mantiene igual
    posFinal = new Vector3(player.position.x - distancia.x, posActual.y, player.position.z - distancia.z);
    this.transform.position = Vector3.Lerp(posActual, posFinal, velocidad );
}
// ...existing code...
}
