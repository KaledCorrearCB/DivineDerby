using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance; // Para acceder desde cualquier otro script()

    [Header("Clips de música")]
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    private AudioSource audioSource;

    private void Awake()
    {
        // Hacer que este objeto no se destruya al cambiar de "estado" (aunque uses misma escena)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayMenuMusic(); 
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    public void PlayGameMusic()
    {
        PlayMusic(gameMusic);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return; // Si ya está sonando, no reinicia

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
