using UnityEngine;

public class Vidas : MonoBehaviour
{
    int vidas = 3;

    public GameObject[] corazonesUI;
    public GameObject[] vidasUI;

    public GameObject menuGameOver; // Asignar en Inspector
    public GameObject botonPausa;   // Asignar en Inspector

    void Start()
    {
        ActualizarUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (vidas > 0)
            {
                vidas--;
                ActualizarUI();

                if (vidas == 0)
                {
                    Time.timeScale = 0;

                    if (menuGameOver != null)
                        menuGameOver.SetActive(true);

                    if (botonPausa != null)
                        botonPausa.SetActive(false);
                }
            }
        }
    }

    public void ActualizarUI()
    {
        // Corazones simples
        for (int i = 0; i < corazonesUI.Length; i++)
        {
            corazonesUI[i].SetActive(i < vidas);
        }

        // Imágenes animadas
        for (int i = 0; i < vidasUI.Length; i++)
        {
            vidasUI[i].SetActive(false);
        }

        if (vidas > 0 && vidas <= vidasUI.Length)
        {
            GameObject vidaActiva = vidasUI[vidas - 1];

            vidaActiva.SetActive(false); // reinicia
            vidaActiva.SetActive(true);

            ImageSwapper2 swapper = vidaActiva.GetComponent<ImageSwapper2>();
            if (swapper != null)
            {
                swapper.RestartAnimation();
            }
        }
    }

    public int GetVidas()
    {
        return vidas;
    }
}
