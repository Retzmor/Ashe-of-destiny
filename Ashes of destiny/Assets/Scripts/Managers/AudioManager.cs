using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource loopSource;

    string musicKey = "MusicVolume";
    string masterKey = "MasterVolume";
    string sfxKey = "SFXVolume";

    float musicVolume;
    float sfxVolume;
    float masterVolume;
    public void Start()
    {
        ChangeMusicVolume(PlayerPrefs.HasKey(musicKey) ? PlayerPrefs.GetFloat(musicKey) : 1);
        ChangeMasterVolume(PlayerPrefs.HasKey(masterKey) ? PlayerPrefs.GetFloat(masterKey) : 1);
        ChangeSFXVolume(PlayerPrefs.HasKey(sfxKey) ? PlayerPrefs.GetFloat(sfxKey) : 1);
    }

    public void ChangeMusicVolume(float currentVolume)
    {
        musicVolume = currentVolume;
        PlayerPrefs.SetFloat(musicKey, currentVolume);
        audioMixer.SetFloat(musicKey, Decibel(currentVolume));
    }

    public void ChangeMasterVolume(float currentVolume)
    {
        masterVolume = currentVolume;
        PlayerPrefs.SetFloat(masterKey, currentVolume);
        audioMixer.SetFloat(masterKey, Decibel(currentVolume));
    }

    public void ChangeSFXVolume(float currentVolume)
    {
        sfxVolume = currentVolume;
        PlayerPrefs.SetFloat(sfxKey, currentVolume);
        audioMixer.SetFloat(sfxKey, Decibel(currentVolume));
    }

    public float Decibel(float volumen)
    {
        return Mathf.Log10(volumen) * 20f;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetMasterVolume()
    {
        return masterVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }
    public void PlaySFX(AudioClip clip, float volume)
    {
        if (clip == null) return;

        sfxSource.pitch = Random.Range(0.75f, 1.05f);
        sfxSource.PlayOneShot(clip, volume);
    }


    public void PlaySFX3D(AudioClip clip, Vector3 position)
    {
        if (clip == null) return;

        AudioSource.PlayClipAtPoint(clip, position);
    }
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayLoop(AudioClip clip)
    {
        if (clip == null) return;

        if (loopSource.clip == clip) return;

        loopSource.clip = clip;
        loopSource.loop = true;
        loopSource.Play();
    }
    public void StopLoop()
    {
        loopSource.Stop();
    }

}

