using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 Pointerposition { get; set; }

    private void Update()
    {
        Pointerposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = Pointerposition - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Detecta si el personaje está "mirando hacia la izquierda" (escalado negativamente)
        bool estaVolteado = transform.lossyScale.x < 0;

        // Si está volteado, invierte el ángulo verticalmente
        if (estaVolteado)
        {
            angle += 180f;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
