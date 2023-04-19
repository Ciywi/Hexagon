using UnityEngine;
using UnityEngine.UI;
using Nojumpo.ScriptableObjects;

namespace Nojumpo.UI
{
    public class SliderSetter : MonoBehaviour
    {
        #region Fields

        [Tooltip("Slider to set")]
        [SerializeField] private Slider _slider;

        [Tooltip("Float Variable Scriptable Object Value to equalize to the Slider value")]
        [SerializeField] private FloatVariableSO _variable;

        #endregion

        #region Update

        private void Update()
        {
            if (_slider != null && _variable != null)
            {
                _slider.value = _variable.Value;
            }
        }

        #endregion
    }
}