using Nojumpo.ScriptableObjects;
using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class AudioSettings
{

    // Change Class Name as Mixer Settings
    #region Serialized Fields

    [SerializeField] private string _exposedMixerName;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private FloatVariableSO _volumeAmount;

    #endregion


    #region Private Fields



    #endregion


    #region Properties

    public string VolumeName { get { return _exposedMixerName; } set { _exposedMixerName = value; } }
    public AudioMixer AudioMixer { get { return _audioMixer; } set { _audioMixer = value; } }
    public FloatVariableSO VolumeAmount { get { return _volumeAmount; } set { _volumeAmount = value; } }

    #endregion
}
