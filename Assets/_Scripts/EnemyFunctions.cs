using UnityEngine;

public class EnemyFunctions : MonoBehaviour
{
    public int health = 100; // Salud del enemigo

    public void TakeDamage(int damage)
    {
        health -= damage; // Resta el daño a la salud del enemigo
        if (health <= 0)
        {
            Die(); // Llama a la función de muerte si la salud es 0 o menos
        }
    }
    private void Die()
    {
        Destroy(gameObject); // Destruye el objeto enemigo
    }

}
