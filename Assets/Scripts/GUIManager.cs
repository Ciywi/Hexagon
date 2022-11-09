using System;
using System.Collections;
using System.Collections.Generic;
using TheraBytes.BetterUi;
using TMPro;
using UnityEngine;

namespace Managers
{

    public class GUIManager : MonoBehaviour
    {
        #region Instance

        public static GUIManager Instance;

        #endregion

        [Header("Components")]
        #region Serialized Fields

        [SerializeField] private CanvasGroup _gameOverPanel;
        [SerializeField] private CanvasGroup _pauseMenuPanel;

        #endregion

        #region Properties

        public CanvasGroup GameOverPanel { get { return _gameOverPanel; } }
        public CanvasGroup PauseMenuPanel { get { return _pauseMenuPanel; } }

        #endregion

        [Header("Best Time Texts")]
        #region Serialized Fields

        [SerializeField] private BetterTextMeshProUGUI _lastGameTimeText;
        [SerializeField] private BetterTextMeshProUGUI _yourNewBestText;
        [SerializeField] private BetterTextMeshProUGUI _bestTimeTextOnGUI;

        #endregion

        #region Properties

        public BetterTextMeshProUGUI YourNewBestText { get { return _yourNewBestText; } set { _yourNewBestText = value; } }

        #endregion

        #region Awake and Start

        private void Awake()
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


        private void Start()
        {
            _bestTimeTextOnGUI.text = $"Best Time {GameManager.Instance.BestTime:0.0}";
        }

        #endregion


        #region Public Methods

        public void GameTimeText()
        {
            _lastGameTimeText.text = $"You Dodged For \n {TimeManager.Instance.CurrentTime:0.0} Seconds";
            _bestTimeTextOnGUI.text = $"Best Time {GameManager.Instance.BestTime:0.0}";
        }

        public void ActivateCanvasGroup(CanvasGroup canvasGroup, bool activate)
        {
            if (activate == true)
            {
                canvasGroup.alpha = 1;
            }
            else
            {
                canvasGroup.alpha = 0;
            }

            canvasGroup.blocksRaycasts = activate;
            canvasGroup.interactable = activate;

        }

        #endregion
    }

}

