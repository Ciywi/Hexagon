using System;
using UnityEngine;

namespace Nojumpo
{
    public class ButtonBase : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        AudioSource _buttonAudioSource;
        
        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            _buttonAudioSource = GetComponent<AudioSource>();
        }

        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void PlayAudio(AudioClip audioClip) {
            _buttonAudioSource.clip = audioClip;
            _buttonAudioSource.Play();
        }
    }
}