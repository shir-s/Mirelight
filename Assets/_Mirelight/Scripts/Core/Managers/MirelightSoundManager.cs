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
    public AudioClip loseClip;
    public AudioClip winClip;

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

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.Stop();
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

    public void StopAllSounds()
    {
        if (musicSource != null)
            musicSource.Stop();

        if (sfxSource != null)
            sfxSource.Stop();
    }

    public void PlayLoseSound()
    {
        if (loseClip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(loseClip);
        }
    }
    
    public void PlayWinSound()
    {
        if (winClip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(winClip);
        }
    }
}