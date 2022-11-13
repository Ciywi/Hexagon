using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{

    public class ButtonManager : MonoBehaviour
    {
        #region Instance

        public static ButtonManager Instance;

        #endregion

        AudioManager _audioManager;

        #region Awake

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        #endregion

        public void PlayButton()
        {
            GameManager.Instance.PlayButton();
        }

        public void StartGame()
        {
            GameManager.Instance.StartGame();
        }

        public void ExitButton()
        {
            GameManager.Instance.ExitGame();
        }

        public void PauseButton()
        {
            GameManager.Instance.PauseGame();
        }

        public void RestartButton()
        {
            GameManager.Instance.RestartGame();
        }

        public void WatchAdButton()
        {
            GameManager.Instance.WatchAdToContinue();
        }

        public void PlayAudio(string audioName)
        {
            _audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
            _audioManager.PlayAudio(audioName);
        }
    }

}

