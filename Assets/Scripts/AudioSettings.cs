using System;
using TheraBytes.BetterUi;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class AudioSettings
{

    // Change Class Name as Mixer Settings
    #region Serialized Fields

    [SerializeField] private string _volumeName;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private BetterSlider _slider;

    #endregion


    #region Private Fields



    #endregion


    #region Properties

    public string VolumeName { get { return _volumeName; } set { _volumeName = value; } }
    public AudioMixer AudioMixer { get { return _audioMixer; } set { _audioMixer = value; } }
    public BetterSlider Slider { get { return _slider; } set { _slider = value; } }

    #endregion
}
