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
        ColorLerp(_hexagonLineRenderer);
    }

    void Shrink()
    {
        transform.localScale -= Vector3.one * GameManager.Instance.ShrinkSpeed * Time.deltaTime;

        if (transform.localScale.x <= 0.5f && _resized == true)
        {
            AnimationManager.Instance.GlowAnimation(ReferenceManager.Instance.CenterHexagon, 0.1f, 2);
            CinemachineCameraShake.Instance.ShakeCamera(1.5f, 0.5f);
            Invoke(nameof(ResizerAndRotater), _resizeDelay);
            _resized = false;
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


    public void SetMaterialColor(LineRenderer renderer, Color newColor)
    {
        renderer.material.color = newColor;
    }

    private void ResizerAndRotater()
    {
        transform.localScale = Vector3.one * _startingSize;
        RotationRandomizer();
        GameManager.Instance.ShrinkSpeedUp(0.05f);
        _resized = true;
    }

    private void  ColorLerp(LineRenderer renderer)
    {
        renderer.material.color = Color.Lerp(renderer.material.color, _lerpColors[_colorIndex], _lerpTime * Time.deltaTime);

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
            SetMaterialColor(_hexagonLineRenderer, _red);
            AudioManager.Instance.DecrementAudioPitch("Game Music", 0.8f);
            StartCoroutine(GameManager.Instance.GameOver());
            CinemachineCameraShake.Instance.ShakeCamera(6, 0.20f);
        }
    }

    #endregion
}
