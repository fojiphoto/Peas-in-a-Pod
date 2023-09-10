using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonobehaviourSingleton<AudioManager>
{

    [SerializeField] private AudioSource soundEffectSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip BackGroudMusic;
    [SerializeField] public AudioClip Tap;
    [SerializeField] public AudioClip Match;
    [SerializeField] public AudioClip MissMatch;

    private void Start()
    {
        PlayMusic(BackGroudMusic);
        SetMusicVolume(0.75f);
    }
    public void PlaySoundEffect(AudioClip clip)
    {
        soundEffectSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSoundEffectsVolume(float volume)
    {
        soundEffectSource.volume = volume;
    }
}
