using UnityEngine;

public class AudioManager : MonoBehaviour
{   

    public AudioClip coinClip;
    public AudioClip explosionClip;
    public AudioClip missileClip;
    public AudioClip backgroundMusic;    
    public AudioClip powerUpClip;

    private AudioSource backgroundMusicSource;
    private AudioSource soundEffectSource;

    private void Awake()
    {             
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        soundEffectSource = gameObject.AddComponent<AudioSource>();

        
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    public void PlayExplosionSound()
    {
        PlaySound(explosionClip, soundEffectSource);
    }

    public void PlayMissileSound()
    {
        PlaySound(missileClip, soundEffectSource);
    }

    public void PlayCoinSound()
    {
        PlaySound(coinClip, soundEffectSource);
    }

    public void PlayPowerUpSound()
    {
        PlaySound(powerUpClip, soundEffectSource);
    }

    private void PlaySound(AudioClip clip, AudioSource source)
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }

    public void StopAllSounds()
    {
        soundEffectSource.Stop();
    }
}
