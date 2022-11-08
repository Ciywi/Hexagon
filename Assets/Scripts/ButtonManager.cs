using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{

    public class ButtonManager : MonoBehaviour
    {
        #region Instance

        public static ButtonManager Instance;

        #endregion


        #region Awake and Start

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

        void Start()
        {
            
        }

        #endregion

        public void PlayButton()
        {
            GameManager.Instance.PlayButton();
        }

        public void ExitButton()
        {
            GameManager.Instance.ExitGame();
        }

        public void ResumeButton()
        {
            GameManager.Instance.ResumeGame();
        }
    }

}

