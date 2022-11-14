using TheraBytes.BetterUi;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{

    public class UIManager : MonoBehaviour
    {
        #region Instance

        public static UIManager Instance;

        #endregion

        #region Fields

        [Header("Components")]
        #region Private Fields

        private BetterToggle _settingsToggle;
        private CanvasGroup _settingCanvas;

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
            _settingsToggle = GameObject.Find("Settings Toggle").GetComponent<BetterToggle>();
            _settingCanvas = GameObject.Find("Settings Panel").GetComponent<CanvasGroup>();

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

