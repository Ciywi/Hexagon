using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    #region Instance

    public static AudioManager Instance;

    #endregion

    #region Fields

    [Header("Audio Components")]
    #region Serialized Fields

    [SerializeField] private Sounds[] _sounds;

    #endregion

    #endregion


    private void Awake()
    {
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

        foreach (Sounds sound in _sounds)
        {
            sound.SoundSource = gameObject.AddComponent<AudioSource>();

            sound.SoundSource.clip = sound.Clip;
            sound.SoundSource.volume = sound.Volume;
            sound.SoundSource.pitch = sound.Pitch;
        }
    }

    private void Start()
    {
        PlayAudio("Menu Music");
    }

    public void PlayAudio(string name)
    {
        Sounds audio = Array.Find(_sounds, sound => sound.Name == name);

        if (audio == null)
        {
            Debug.Log($"There is no audio named '{name}'");
            return;
        }

        audio.SoundSource.Play();
    }

    public void LowerAudioPitch(string name, float startValue, float endValue, float speed)
    {
        Sounds audio = Array.Find(_sounds, sound => sound.Name == name);
        audio.SoundSource.pitch = Mathf.MoveTowards(startValue, endValue, speed * Time.unscaledTime);
    }

    public void StopAudio(string name)
    {
        Sounds audio = Array.Find(_sounds, sound => sound.Name == name);
        audio.SoundSource.Stop();
    }

    public void RestartAudio(string name)
    {
        Sounds audio = Array.Find(_sounds, sound => sound.Name == name);
        audio.SoundSource.Stop();
        audio.SoundSource.pitch = 1;
        audio.SoundSource.Play();
    }

}
