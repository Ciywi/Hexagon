using TheraBytes.BetterUi;
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
        #region Private Fields

        private BetterToggle _settingsToggle;
        private CanvasGroup _settingsCanvas;

        #endregion


        #endregion

        #region Awake and Start

        void Awake() {
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

        public void ActivateSettingsCanvasGroup() {
            _settingsToggle = GameObject.Find("Settings Toggle").GetComponent<BetterToggle>();
            _settingsCanvas = GameObject.Find("Settings Panel").GetComponent<CanvasGroup>();

            if (_settingsToggle.isOn)
            {
                _settingsCanvas.alpha = 1;
            }
            else
            {
                _settingsCanvas.alpha = 0;
            }

            _settingsCanvas.blocksRaycasts = _settingsToggle.isOn;
            _settingsCanvas.interactable = _settingsToggle.isOn;
        }

        public void ActivateCanvasGroup(CanvasGroup canvasGroup, bool activate) {
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

