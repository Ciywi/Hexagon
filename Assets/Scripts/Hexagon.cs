using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Managers;

public class Hexagon : MonoBehaviour
{
    #region Instance

    public static Hexagon Instance;

    #endregion

    [Header("Components")]
    #region Private Fields

    private Rigidbody2D _hexagonRigidbody;
    private LineRenderer _hexagonLineRenderer;

    #endregion


    [Header("Trigger Settings")]
    #region Private Fields

    private Color _red = Color.red;

    #endregion

    #region Properties

    public LineRenderer HexagonLineRenderer { get { return _hexagonLineRenderer; } set { _hexagonLineRenderer = value; } }

    #endregion

    [Header("Shrink Settings")]
    #region Serialized Fields

    [SerializeField] private float _startingSize = 15f;

    #endregion

    #region Private Fields

    private bool _resized = true;

    #endregion

    [Header("Resize Settings")]
    #region Serialized Field

    [SerializeField] private float _resizeDelay = 0.01f;

    #endregion

    [Header("Rotation Settings")]
    #region Serialized Fields

    [SerializeField] private int[] _rotations = new int[6];

    #endregion

    [Header("Color Lerp Settings")]
    #region Serialized Fields

    [SerializeField][Range(0f, 1f)] private float _lerpTime;
    [SerializeField] Color[] _lerpColors;

    #endregion

    #region Private Fields

    Color _initialColor;

    #endregion

    #region Private Fields

    private int _colorIndex = 0;
    private int _colorArrayLength;
    private float _timeToLerp = 0.0f;

    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _hexagonRigidbody = GetComponent<Rigidbody2D>();
        _hexagonLineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        SetRotations();
        RotationRandomizer();
        transform.localScale = Vector3.one * _startingSize;
        _colorArrayLength = _lerpColors.Length;
    }
    void Update()
    {
        Shrink();
        ColorLerp();
    }

    void Shrink()
    {
        transform.localScale -= Vector3.one * GameManager.Instance.ShrinkSpeed * Time.deltaTime;

        if (transform.localScale.x <= 0.5f && _resized == true)
        {
            AnimationManager.Instance.ScaleUpAndDownAnimation(ReferenceManager.Instance.CenterHexagon);
            Invoke(nameof(ResizerAndRotater), _resizeDelay);
            _resized = false;
            GameManager.Instance.ShrinkSpeedUp(0.05f);
            Debug.Log($"Shrink Speed is {GameManager.Instance.ShrinkSpeed}");
        }
    }

    #region Private Methods

    void SetRotations()
    {
        int angle = 0;

        for (int i = 0; i < _rotations.Length; i++)
        {
            _rotations[i] = 0 + angle;
            angle += 60;
        }
    }

    void RotationRandomizer()
    {
        int i = Random.Range(0, _rotations.Length - 1);

        _hexagonRigidbody.rotation = _rotations[i];
    }


    public void SetHexagonColor(Color newColor)
    {
        _hexagonLineRenderer.startColor = newColor;
        _hexagonLineRenderer.endColor = newColor;
    }

    public void SetHexagonColor2(Color newColor)
    {
        Gradient tempGradient = new Gradient();
        GradientColorKey[] tempColorKeys = new GradientColorKey[2];

        tempColorKeys[0] = new GradientColorKey(newColor, 0);
        tempColorKeys[1] = new GradientColorKey(newColor, 1);

        tempGradient.colorKeys = tempColorKeys;
        _hexagonLineRenderer.colorGradient = tempGradient;


    }
    private void ResizerAndRotater()
    {
        transform.localScale = Vector3.one * _startingSize;
        RotationRandomizer();
        _resized = true;
    }

    private void  ColorLerp()
    {
        //Color initialColor = _hexagonLineRenderer.colorGradient.colorKeys[0].color;

        //SetHexagonColor2(initialColor);

        //while (true)
        //{
        //    initialColor = _hexagonLineRenderer.colorGradient.colorKeys[0].color;
        //    Color targetColor = _lerpColors[_colorIndex];

        //    Color currentColor = _initialColor;
        //    currentColor = Color.Lerp(currentColor, _lerpColors[_colorIndex], _lerpTime * Time.deltaTime);
        //    SetHexagonColor(currentColor);
        //    _timeToLerp = Mathf.Lerp(_timeToLerp, 1f, _lerpTime * Time.deltaTime);

        //    if (_timeToLerp > 0.95f)
        //    {
        //        _timeToLerp = 0f;
        //        _colorIndex++;
        //        _colorIndex = (_colorIndex >= _colorArrayLength) ? 0 : _colorIndex;
        //        yield return null;
        //    }
        //    yield return null;
        //}

        _hexagonLineRenderer.material.color = Color.Lerp(_hexagonLineRenderer.material.color, _lerpColors[_colorIndex], _lerpTime * Time.deltaTime);

        _timeToLerp = Mathf.Lerp(_timeToLerp, 1f, _lerpTime * Time.deltaTime);

        if (_timeToLerp > 0.95f)
        {
            _timeToLerp = 0f;
            _colorIndex++;
            _colorIndex = (_colorIndex >= _colorArrayLength) ? 0 : _colorIndex;
        }
    }

    #endregion

    #region Triggers

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlayAudio("Hexagon Hit Sound Effect");
            SetHexagonColor(_red);
            AudioManager.Instance.LowerAudioPitch("Game Music", 1.0f, 0.85f, 0.05f);
            StartCoroutine(GameManager.Instance.GameOver());
        }
    }

    #endregion
}
