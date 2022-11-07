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

    [Header("Trigger Settings")]
    #region Private Fields

    private Color _red = Color.red;

    #endregion

    #region Private Fields

    private Rigidbody2D _hexagonRigidbody;
    private LineRenderer _hexagonLineRenderer;

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
    }
    void Update()
    {

        Shrink();
    }

    void Shrink()
    {
        transform.localScale -= Vector3.one * GameManager.Instance.ShrinkSpeed * Time.deltaTime;

        if (transform.localScale.x <= 0.5f && _resized == true)
        {
            AnimationManager.Instance.ScaleUpAndDownAnimation(ReferenceManager.Instance.CenterHexagon);
            Invoke(nameof(ResizerAndRotater), _resizeDelay);
            _hexagonLineRenderer.enabled = false;
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



    public void HexagonColorChangerOnHit(Color colorToBe)
    {
        _hexagonLineRenderer.startColor = colorToBe;
        _hexagonLineRenderer.endColor = colorToBe;
    }
    private void ResizerAndRotater()
    {
        transform.localScale = Vector3.one * _startingSize;
        RotationRandomizer();
        _hexagonLineRenderer.enabled = true;
        _resized = true;
    }

    #endregion

    #region Triggers

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlayAudio("Hexagon Hit Sound Effect");
            HexagonColorChangerOnHit(_red);
            AudioManager.Instance.LowerAudioPitch("Game Music", 1.0f, 0.85f, 0.05f);
            StartCoroutine(GameManager.Instance.GameOver());
        }
    }

    #endregion
}
