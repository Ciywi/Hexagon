using System;
using UnityEngine;

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

    private void Awake() {
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

            if (sound.Name == "Game Music")
            {
                sound.AudioSpectrum = gameObject.AddComponent<AudioSpectrum>();
            }
        }
    }

    private void Start() {
        SetMixerVolume("Master");
        SetMixerVolume("Music");
        SetMixerVolume("SFX");
        PlayAudio("Menu Music");
    }

    #endregion

    #region Public Methods

    public void PlayAudio(string name) {
        Sounds audio = Array.Find(_sounds, sound => sound.Name == name);

        if (audio == null)
        {
            Debug.Log($"There is no audio named '{name}'");
            return;
        }

        audio.SoundSource.Play();
    }

    public void StopAudio(string audioName) {
        Sounds audio = Array.Find(_sounds, sound => sound.Name == audioName);
        audio.SoundSource.Stop();
    }

    public void PauseAudio(string audioName) {
        Sounds audio = Array.Find(_sounds, sound => sound.Name == audioName);
        audio.SoundSource.Pause();
    }

    public void RestartAudio(string audioName) {
        Sounds audio = Array.Find(_sounds, sound => sound.Name == audioName);
        audio.SoundSource.Stop();
        audio.SoundSource.pitch = 1;
        audio.SoundSource.Play();
    }

    public void SetAudioPitch(string audioName, float endPitchValue) {
        Sounds audio = Array.Find(_sounds, sound => sound.Name == audioName);
        audio.SoundSource.pitch = endPitchValue;
    }

    public void IncrementAudioPitch(string audioName, float incrementValue) {
        Sounds audio = Array.Find(_sounds, sound => sound.Name == audioName);
        audio.SoundSource.pitch += incrementValue;
    }

    public void SetMixerVolume(string mixerName) {
        AudioSettings audioSettings = Array.Find(_audioSettings, audioSettings => audioSettings.VolumeName == mixerName);

        float volume = audioSettings.VolumeAmount.Value;

        audioSettings.AudioMixer.SetFloat(mixerName, Mathf.Log(volume) * 20f);
    }

    #endregion
}
