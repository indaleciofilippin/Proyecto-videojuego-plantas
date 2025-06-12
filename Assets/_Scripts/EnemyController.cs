using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sRenderer;

    [SerializeField]
    private bool moveLeft = true;

    [SerializeField]
    private float speed = 2f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        SetVelocity();
    }

    private void SetVelocity()
    {
        if (moveLeft)
        {
            rb.linearVelocity = Vector2.left * speed;
            sRenderer.flipX = false; // Suponiendo que el sprite ya mira a la izquierda por defecto
        }
        else
        {
            rb.linearVelocity = Vector2.right * speed;
            sRenderer.flipX = true; // Se da vuelta si va a la derecha
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flag"))
        {
            moveLeft = !moveLeft;
            SetVelocity();
        }
    }
}
