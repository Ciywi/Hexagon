using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    #region Fields

    [SerializeField] string _name;
    [SerializeField] AudioClip _clip;

    [SerializeField][Range(0.0f, 1.0f)] float _volume = 1.0f;
    [SerializeField][Range(0.0f, 1.0f)] float _pitch = 1.0f;

    [SerializeField] private bool _loop = false;

    [SerializeField] private AudioMixerGroup _mixerGroup;

    private AudioSource _soundSource;
    private AudioSpectrum _audioSpectrum;

    #endregion

    #region Properties

    public string Name { get { return _name; } set { _name = value; } }
    public float Volume { get { return _volume; } set { _volume = value; } }
    public float Pitch { get { return _pitch; } set { _pitch = value; } }
    public bool Loop { get { return _loop; } set { _loop = value; } }
    public AudioSource SoundSource { get { return _soundSource; } set { _soundSource = value; } }
    public AudioClip Clip { get { return _clip; } set { _clip = value; } }
    public AudioMixerGroup MixerGroup { get { return _mixerGroup; } set { _mixerGroup = value; } }
    public AudioSpectrum AudioSpectrum { get { return _audioSpectrum; } set { _audioSpectrum = value; } }

    #endregion

}
