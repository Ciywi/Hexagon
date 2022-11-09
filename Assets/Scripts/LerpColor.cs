using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpColor : MonoBehaviour
{
    #region Instance

    public static LerpColor Instance;

    #endregion

    [Header("Components")]
    #region Private Region

    private MeshRenderer _gameObjectMeshRenderer;

    #endregion

    [Header("Color Lerp Settings")]
    #region Serialized Fields

    [SerializeField][Range(0f, 1f)] private float _lerpTime;
    [SerializeField] Color[] _lerpColors;
    [SerializeField] Color _objectColor;

    #endregion

    #region Private Fields

    private int _colorIndex = 0;
    private int _colorArrayLength;
    private float _timeToLerp = 0.0f;

    #endregion

    #region Awake and Start

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _colorArrayLength = _lerpColors.Length;
    }
    #endregion

    #region Update

    private void Update()
    {
        ColorLerp(_objectColor);
    }

    #endregion

    #region Private Methods

    private void ColorLerp(Color color)
    {
        color = Color.Lerp(color, _lerpColors[_colorIndex], _lerpTime * Time.deltaTime);

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
