using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager sManager;
    public static AudioManager Singleton
    {
        get
        {
            if (sManager == null)
                sManager = FindObjectOfType<AudioManager>();
            return sManager;
        }
    }

    public AudioClip[] soundEffect;

    private AudioSource musicAudioEmitter;
    private AudioSource soundAudioEmitter;

    private float mutedMusicVolume;
    private float mutedSoundVolume;

    private void Start()
    {
        musicAudioEmitter = GameObject.FindGameObjectWithTag("MusicEmitter").GetComponent<AudioSource>();
        soundAudioEmitter = GameObject.FindGameObjectWithTag("SoundEmitter").GetComponent<AudioSource>();
    }

    public void MuteMusic(bool musicMuted)
    {
        if (!musicMuted)
        {
            mutedMusicVolume = musicAudioEmitter.volume;
            musicAudioEmitter.volume = 0;
        }
        else
            musicAudioEmitter.volume = mutedMusicVolume;
    }

    public void MuteSound(bool soundMuted)
    {
        if (!soundMuted)
        {
            mutedSoundVolume = soundAudioEmitter.volume;
            soundAudioEmitter.volume = 0;
        }
        else
            soundAudioEmitter.volume = mutedSoundVolume;
    }

    public void ChangeMusicVolume(float volume)
    {
        musicAudioEmitter.volume = volume;
    }

    public void ChangeSoundVolume(float volume)
    {
        soundAudioEmitter.volume = volume;
    }

    public void PlaySoundEffect(int effectNumber)
    {
        soundAudioEmitter.PlayOneShot(soundEffect[effectNumber]);
    }

    public void PlaySoundEffectOnLoop(int effectNumber)
    {
        soundAudioEmitter.clip = soundEffect[effectNumber];
        soundAudioEmitter.Play();
    }

    public void StopSoundEffect()
    {
        soundAudioEmitter.clip = null;
    }
}
