using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using DG.Tweening;
using UnityEngine.UIElements;
using TheraBytes.BetterUi;
using static UnityEngine.Rendering.DebugUI;

public class AudioManager : MonoBehaviour
{
    #region Instance

    public static AudioManager Instance;

    #endregion

    #region Fields

    [Header("Audio Components")]
    #region Serialized Fields

    [SerializeField] private Sounds[] _sounds;
    [SerializeField] private AudioSettings[] _audioSettings;

    #endregion

    #endregion

    #region Awake and Start

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
            sound.SoundSource.outputAudioMixerGroup = sound.MixerGroup;
        }
    }

    private void Start()
    {
        PlayAudio("Menu Music");
    }

    #endregion

    #region Public Methods

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

    public void StopAudio(string audioName)
    {
        Sounds audio = Array.Find(_sounds, sound => sound.Name == audioName);
        audio.SoundSource.Stop();
    }

    public void PauseAudio(string audioName)
    {
        Sounds audio = Array.Find(_sounds, sound => sound.Name == audioName);
        audio.SoundSource.Pause();
    }

    public void RestartAudio(string audioName)
    {
        Sounds audio = Array.Find(_sounds, sound => sound.Name == audioName);
        audio.SoundSource.Stop();
        audio.SoundSource.pitch = 1;
        audio.SoundSource.Play();
    }

    public void LowerAudioPitch(string audioName, float startValue, float endValue, float speed)
    {
        Sounds audio = Array.Find(_sounds, sound => sound.Name == audioName);
        audio.SoundSource.pitch = Mathf.MoveTowards(startValue, endValue, speed * Time.unscaledTime);
    }

    public void SetVolume(string mixerName)
    {
        AudioSettings audioSettings = Array.Find(_audioSettings, audioSettings => audioSettings.VolumeName == mixerName);

        float volume = audioSettings.Slider.value;

        audioSettings.AudioMixer.SetFloat(mixerName, Mathf.Log(volume) * 20f);

    }

    #endregion
}
