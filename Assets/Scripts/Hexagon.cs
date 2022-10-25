using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    [SerializeField][Range(1.0f, 25.0f)] private float _shrinkSpeed;


    #endregion

    #region Private Fields

    private bool _resized = true;

    #endregion

    [Header("Resize Settings")]
    #region Serialized Field

    [SerializeField] private float _resizeDelay = 0.2f;

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
        transform.localScale -= Vector3.one * _shrinkSpeed * Time.deltaTime;

        if (transform.localScale.x <= 0.5f && _resized == true)
        {
            AnimationManager.Instance.CenterHexagonAnimation(GameManager.Instance.CenterHexagon);
            StartCoroutine("ResizerAndRotater");
            _hexagonLineRenderer.enabled = false;
            _resized = false;
            ShrinkSpeedUp(0.01f);
            Debug.Log($"Shrink Speed is {_shrinkSpeed}");
        }
    }

    IEnumerator ResizerAndRotater()
    {
        yield return new WaitForSeconds(_resizeDelay);

        transform.localScale = Vector3.one * _startingSize;
        RotationRandomizer();
        _hexagonLineRenderer.enabled = true;
        _resized = true;
        Debug.Log($"Hexagon Rotation is: {_hexagonRigidbody.rotation}");
    }


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
    void ShrinkSpeedUp(float speedUp)
    {
        _shrinkSpeed += speedUp;
    }



    public void HexagonColorChangerOnHit(Color colorToBe)
    {
        _hexagonLineRenderer.startColor = colorToBe;
        _hexagonLineRenderer.endColor = colorToBe;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlayAudio("Hexagon Hit Sound Effect");
            HexagonColorChangerOnHit(_red);
            AudioManager.Instance.LowerAudioPitch("Background Audio", 1.0f, 0.85f, 0.05f);
            StartCoroutine(GameManager.Instance.GameOver());
        }
    }
}
