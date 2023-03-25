using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LerpColor : MonoBehaviour
{
    [Header("Components")]
    #region Private Region

    private TextMeshProUGUI _gameObjectText;

    #endregion

    [Header("Color Lerp Settings")]
    #region Private Fields

    private Color _objectColor;

    #endregion

    #region Serialized Fields

    [SerializeField][Range(0f, 1f)] private float _lerpTime;
    [SerializeField] Color[] _lerpColors;

    #endregion

    #region Private Fields

    private int _colorIndex = 0;
    private int _colorArrayLength;
    private float _timeToLerp = 0.0f;

    #endregion

    #region Awake and Start

    private void Awake()
    {
        _colorArrayLength = _lerpColors.Length;
        _gameObjectText = GetComponent<TextMeshProUGUI>();
    }
    #endregion

    #region Update

    private void Update()
    {
        ColorLerp();
    }

    #endregion

    #region Private Methods

    private void ColorLerp()
    {
        _gameObjectText.color = Color.Lerp(_gameObjectText.color, _lerpColors[_colorIndex], _lerpTime * Time.deltaTime);
        _gameObjectText.fontSharedMaterial.SetColor("_GlowColor", Color.Lerp(_gameObjectText.color, _lerpColors[_colorIndex], _lerpTime * Time.deltaTime));

        _timeToLerp = Mathf.Lerp(_timeToLerp, 1f, _lerpTime * Time.deltaTime);

        if (_timeToLerp > 0.95f)
        {
            _timeToLerp = 0f;
            _colorIndex++;
            _colorIndex = (_colorIndex >= _colorArrayLength) ? 0 : _colorIndex;
        }
    }

    #endregion
}
