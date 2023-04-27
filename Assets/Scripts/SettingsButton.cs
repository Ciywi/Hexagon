using UnityEngine;

namespace Nojumpo
{
    public class SettingsButton : ButtonBase
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField]
        GameObject settingsPanel;

        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void SettingsButtonClick() {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }
}
