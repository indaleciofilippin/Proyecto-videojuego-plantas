using UnityEngine;
using UnityEngine.UI;

public class ImageSwapper : MonoBehaviour
{
    public Image image;
    public Sprite[] sprites; // tus 2 sprites
    public float interval = 0.5f; // tiempo entre cada imagen

    private int index = 0;
    private float timer = 0f;

    void Update()
    {
        if (sprites.Length == 0 || image == null) return;

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            index = (index + 1) % sprites.Length;
            image.sprite = sprites[index];
        }
    }
}
