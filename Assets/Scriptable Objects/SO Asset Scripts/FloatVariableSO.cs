using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewFloatVariable", menuName = "Nojumpo/Scriptable Objects/Datas/Variables/New Float Variable")]
    public class FloatVariableSO : ScriptableObject
    {
#if UNITY_EDITOR

        [Multiline]
        [SerializeField] private string _developerDescription = string.Empty;

#endif

        [Tooltip("FLOAT VALUE TO USE")]
        [SerializeField] private float _value;

        [Header("MIXER VALUE VARIABLES")]
        [SerializeField] private string _exposedMixerName;

        public float Value { get { return _value; } set { this._value = value; } }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void SetValue(float value) {
            Value = value;
        }

        public void SetValue(FloatVariableSO value) {
            Value = value.Value;
        }

        public void ApplyChange(float changeAmount) {
            Value += changeAmount;
        }

        public void ApplyChange(FloatVariableSO changeAmount) {
            Value += changeAmount.Value;
        }

        public void SetMixerValue(float value) {
            Value = value;
            AudioManager.Instance.SetMixerVolume(_exposedMixerName);
        }
    }
}
