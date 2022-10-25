using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds 
{
    [SerializeField] string _name;
    [SerializeField] AudioClip _clip;

    [SerializeField][Range(0.0f, 1.0f)] float _volume;
    [SerializeField][Range(0.0f, 1.0f)] float _pitch;

    private AudioSource _soundSource;


    public AudioSource SoundSource { get { return _soundSource; } set { _soundSource = value; } }
    public AudioClip Clip { get { return _clip; } set { _clip = value; } }
    public float Volume { get { return _volume; } set { _volume = value; } }
    public float Pitch { get { return _pitch; } set { _pitch = value; } }
}
