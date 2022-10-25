using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[ExecuteInEditMode]
public class AudioManager : MonoBehaviour
{
    #region Instance

    public static AudioManager Instance;

    #endregion

    [Header("Audio Components")]
    #region Serialized Fields

    [SerializeField] private Sounds[] _sounds;

    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        foreach (Sounds sound in _sounds)
        {
            sound.SoundSource = gameObject.AddComponent<AudioSource>();

            sound.SoundSource.clip = sound.Clip;
            sound.SoundSource.volume = sound.Volume;
            sound.SoundSource.pitch = sound.Pitch;
        }
    }

    public void LowerAudioPitch(int audioIndex, float startValue, float endValue, float speed)
    {
        _sounds[audioIndex].Pitch = Mathf.MoveTowards(startValue, endValue, speed * Time.unscaledTime);
    }

}
