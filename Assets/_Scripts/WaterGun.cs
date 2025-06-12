using UnityEngine;

public class WaterGun : MonoBehaviour
{
    public GameObject waterDropPrefab;      // Prefab de la gota
    public Transform firePoint;             // Punto de salida de la gota
    public float dropSpeed = 10f;           // Velocidad de la gota

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))   // Click izquierdo o Ctrl
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instanciar la gota en la posición del FirePoint
        GameObject drop = Instantiate(waterDropPrefab, firePoint.position, Quaternion.identity);

        // Calcular dirección hacia donde apunta el arma (del FirePoint al mouse)
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePosition - (Vector2)firePoint.position).normalized;

        // Asignar velocidad en esa dirección
        drop.GetComponent<Rigidbody2D>().linearVelocity = shootDirection * dropSpeed;
    }
}
