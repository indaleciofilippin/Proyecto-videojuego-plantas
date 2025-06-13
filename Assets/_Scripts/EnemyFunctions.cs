using UnityEngine;

public class EnemyFunctions : MonoBehaviour
{
    public int health = 100;
    public float fadeDuration = 1.5f;
    private Material fadeMaterial;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            fadeMaterial = new Material(Shader.Find("Custom/FadeOutShader"));
            fadeMaterial.mainTexture = sr.sprite.texture;
            sr.material = fadeMaterial;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(FadeAndDie());
        }
    }

    private System.Collections.IEnumerator FadeAndDie()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadeMaterial.SetFloat("_Fade", alpha);
            yield return null;
        }
        Destroy(gameObject);
    }
}
