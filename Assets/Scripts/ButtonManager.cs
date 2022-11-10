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

        public void ExitButton()
        {
            GameManager.Instance.ExitGame();
        }

        public void PauseButton()
        {
            GameManager.Instance.PauseGame();
        }
    }

}

