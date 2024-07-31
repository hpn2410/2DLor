using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioScript : MonoBehaviour
{
    public static AudioScript instance;
    public Sound[] musicSounds, sfxSounds;

    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("BackGround");
    }

    public void PlayMusic(string name)
    {
        Sound myMusic = Array.Find(musicSounds, x => x.musicName == name);

        if (myMusic != null)
        {
            musicSource.clip = myMusic.clip;
            musicSource.Play();
        }
        else
            Debug.Log("Sounds Not Found");
    }

    public void PlaySFX(string name)
    {
        Sound mySFX = Array.Find(sfxSounds, x => x.musicName == name);

        if (mySFX != null)
        {
            musicSource.PlayOneShot(mySFX.clip);
        }
        else
            Debug.Log("SFX Not Found");
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void ChangeVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void ChangeSFX(float sfx)
    {
        sfxSource.volume = sfx;
    }
}
