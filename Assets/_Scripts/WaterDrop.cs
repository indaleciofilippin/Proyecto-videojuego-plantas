using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public int damage;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void Launch(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyFunctions enemy = collision.GetComponent<EnemyFunctions>();
        PlantaSpawner planta = collision.GetComponent<PlantaSpawner>();
        if (planta != null)
        {
            planta.CurarPlanta(damage);
        }
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
