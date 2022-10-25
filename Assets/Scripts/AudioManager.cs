using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource _backgroundAudio;

    [Header("Pitch Settings")]
    #region Serialized Fields

    [SerializeField][Range(0, 1)] private float _startValue = 1.0f;
    [SerializeField][Range(0, 1)] private float _endValue = 0.7f;
    [SerializeField][Range(0, 1)] private float _speed = 0.1f;

    #endregion

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
        _backgroundAudio.pitch = Mathf.MoveTowards(_startValue, _endValue, _speed * Time.unscaledTime);
    }

}
