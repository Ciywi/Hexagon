using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo
{
    public class BackToMainMenuButton : MonoBehaviour
    {
        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void BackToMenu() {
            SceneManager.LoadScene(0);
        }
    }
}
