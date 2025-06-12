using UnityEngine;

public class GameMusicPlayer : MonoBehaviour
{
    public AudioSource musicaJuego;

    void Start()
    {
        musicaJuego.Play();
    }
}
