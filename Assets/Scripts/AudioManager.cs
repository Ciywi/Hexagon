using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource _backgroundAudio;
    
    //public AudioSource BackgroundAudio { get { return _backgroundAudio; } set { _backgroundAudio = value; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _backgroundAudio = GetComponent<AudioSource>();
    }

    public void LowerAudioPitch()
    {
        _backgroundAudio.pitch = Mathf.Lerp(1.0f, 0.7f, 0.1f);
    }

}
