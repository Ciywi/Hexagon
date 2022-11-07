using System;
using System.Collections;
using System.Collections.Generic;
using TheraBytes.BetterUi;
using TMPro;
using UnityEngine;

namespace Managers
{

    public class UIManager : MonoBehaviour
    {
        #region Instance

        public static UIManager Instance;

        #endregion

        #region Fields

        [Header("Components")]
        #region Serialized Fields

        [SerializeField] private BetterToggle _settingsToggle;
        [SerializeField] private CanvasGroup _settingCanvas;

        #endregion


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

        #endregion

        #region Public Methods

        public void ActivateSettingsCanvasGroup()
        {

            if (_settingsToggle.isOn)
            {
                _settingCanvas.alpha = 1;
            }
            else
            {
                _settingCanvas.alpha = 0;
            }

            _settingCanvas.blocksRaycasts = _settingsToggle.isOn;
            _settingCanvas.interactable = _settingsToggle.isOn;
        }

        #endregion
    }

}

