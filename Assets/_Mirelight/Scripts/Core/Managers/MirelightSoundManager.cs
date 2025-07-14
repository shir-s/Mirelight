using UnityEngine;

public class MirelightSoundManager : MonoBehaviour
{
    public static MirelightSoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip backgroundMusic;
    public AudioClip typewriterSound;
    public AudioClip collectItemSound;
    public AudioClip playerHitSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayTypewriter()
    {
        PlaySFX(typewriterSound, 0.9f);
    }

    public void PlayCollectItem()
    {
        PlaySFX(collectItemSound, 0.7f);
    }

    public void PlayPlayerHit()
    {
        PlaySFX(playerHitSound, 1f);
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip, volume);
    }
}