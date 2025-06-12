using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;

    // Guárdate la escala original al arrancar
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        Vector3 scale = originalScale;

        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            // Invierte solo el signo de X, conserva la magnitud original
            scale.x = -Mathf.Abs(originalScale.x);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            scale.x = Mathf.Abs(originalScale.x);
        }

        transform.localScale = scale;
    }
}
