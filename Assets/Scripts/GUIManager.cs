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

        private AudioManager _audioManager;

        [Header("Pause Menu Settings")]
        #region Serialized Fields

        [SerializeField] private CanvasGroup _pauseMenuPanel;

        #endregion

        #region Properties

        public CanvasGroup PauseMenuPanel { get { return _pauseMenuPanel; } }

        #endregion

        [Header("Game Over Panel Settings")]
        #region Serialized Fields

        [SerializeField] private CanvasGroup _gameOverPanel;
        [SerializeField] private Transform _gameOverBorderTransform;

        #endregion

        #region Properties

        public CanvasGroup GameOverPanel { get { return _gameOverPanel; } }
        public Transform GameOverBorderTransform { get { return _gameOverBorderTransform; } }

        #endregion


        [Header("Resume Game Countdown Settings")]
        #region Serialized Fields

        [SerializeField] private CanvasGroup _resumeGameCountdownPanel;
        [SerializeField] private TextMeshProUGUI _resumeGameCountdownText;

        #endregion

        [Header("Best Time Texts")]
        #region Serialized Fields

        [SerializeField] private TextMeshProUGUI _lastGameTimeText;
        [SerializeField] private TextMeshProUGUI _yourNewBestText;
        [SerializeField] private TextMeshProUGUI _bestTimeTextOnGUI;

        #endregion

        #region Properties

        public TextMeshProUGUI YourNewBestText { get { return _yourNewBestText; } set { _yourNewBestText = value; } }

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

        public void GameTimeTextUpdate()
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

        public void StartResumeGameCountdown()
        {
            StartCoroutine(ResumeGameCountdown(_pauseMenuPanel,"ResumeGame"));
        }

        #endregion

        #region Coroutines

        #region Public

        public IEnumerator ResumeGameCountdown(CanvasGroup panelToClose, string methodToCall)
        {
            _audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
            int i = 3;

            _audioManager.PlayAudio("Countdown");
            _resumeGameCountdownText.text = $"{i}";
            _resumeGameCountdownText.color = Color.green;
            ActivateCanvasGroup(panelToClose, false);
            ActivateCanvasGroup(_resumeGameCountdownPanel, true);

            yield return new WaitForSecondsRealtime(1);
            i--;
            _audioManager.PlayAudio("Countdown");
            _resumeGameCountdownText.text = $"{i}";
            _resumeGameCountdownText.color = Color.yellow;

            yield return new WaitForSecondsRealtime(1);
            i--;
            _audioManager.PlayAudio("Countdown");
            _resumeGameCountdownText.text = $"{i}";
            _resumeGameCountdownText.color = Color.red;

            yield return new WaitForSecondsRealtime(1);
            ActivateCanvasGroup(_resumeGameCountdownPanel, false);

            if (methodToCall == "ResumeGame")
            {
                GameManager.Instance.ResumeGame();
            }
            if (methodToCall == "ContinueGameAfterAD")
            {
                GameManager.Instance.ContinueGameAfterAD();
            }

        }

        #endregion

        #endregion
    }

}

