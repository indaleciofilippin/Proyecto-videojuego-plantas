using UnityEngine;
using UnityEngine.UI;

public class ImageSwapper2 : MonoBehaviour
{
    public Image image;
    public Sprite[] sprites;
    public float interval = 0.5f;

    private int index = 0;
    private float timer = 0f;

    void OnEnable()
    {
        RestartAnimation();
    }

    public void RestartAnimation() // Este es el método que faltaba
    {
        index = 0;
        timer = 0f;
        if (sprites.Length > 0 && image != null)
        {
            image.sprite = sprites[0];
        }

         
    }

    void Update()
    {
        if (sprites.Length == 0 || image == null) return;

        timer += Time.unscaledDeltaTime; // Funciona incluso con Time.timeScale = 0
        if (timer >= interval)
        {
            timer = 0f;
            index = (index + 1) % sprites.Length;
            image.sprite = sprites[index];
        }
    }
}

