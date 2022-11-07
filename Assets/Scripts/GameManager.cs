using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Managers;

public class GameManager : MonoBehaviour
{
    #region Instance

    public static GameManager Instance;

    #endregion

    #region Fields

    [Header("Game Settings")]
    #region Private Fields

    private float _startGameDelay = 0.2f;
    private bool _isGameOver = false;

    #endregion

    [Header("Best Time Settings")]
    #region Private Fields

    private float _bestTime = 0.0f;

    #endregion

    #region Properties

    public float BestTime { get { return _bestTime; } }

    #endregion

    [Header("Shrink Settings")]
    #region Private Fields

    private float _startingShrinkSpeed;

    #endregion
    #region Serialized Fields

    [SerializeField][Range(1.0f, 25.0f)] private float _shrinkSpeed;

    #endregion

    #region Properties

    public float ShrinkSpeed { get { return _shrinkSpeed; } set { _shrinkSpeed = value; } }

    #endregion

    [Header("Color Variables")]
    #region Private Fields

    private Color _white = Color.white;

    #endregion

    #endregion

    #region Awake
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    #endregion

    private void Update()
    {
        if (_isGameOver)
        {
            if (Input.anyKeyDown)
            {
                RestartGame();
            }
        }
    }

    #region Private Methods
    private void StartGame()
    {
        _startingShrinkSpeed = _shrinkSpeed;
        SceneManager.LoadScene(1);
        AudioManager.Instance.PlayAudio("Game Music");
    }

    private void PauseGame()
    {

    }

    private void RestartGame()
    {
        GUIManager.Instance.ActivateCanvasGroup(GUIManager.Instance.GameOverCanvas, false);
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
        ShrinkSpeed = _startingShrinkSpeed;
        AudioManager.Instance.RestartAudio("Game Music");
        _isGameOver = false;
    }

    #endregion

    #region Public Methods

    public void PlayButton()
    {
        Invoke(nameof(StartGame), _startGameDelay);
        AudioManager.Instance.StopAudio("Menu Music");
    }

    public void ShrinkSpeedUp(float speedUp)
    {
        ShrinkSpeed += speedUp;
    }

    public void PersonalRecord()
    {
        if (TimeManager.Instance.CurrentTime > _bestTime)
        {
            _bestTime = TimeManager.Instance.CurrentTime;
        }
    }

    #endregion

    #region Coroutines

    public IEnumerator GameOver()
    {
        Time.timeScale = 0.2f;

        yield return new WaitForSecondsRealtime(2.0f);

        Time.timeScale = 0f;
        AudioManager.Instance.StopAudio("Game Music");
        PersonalRecord();
        GUIManager.Instance.GameTimeText();
        GUIManager.Instance.ActivateCanvasGroup(GUIManager.Instance.GameOverCanvas, true);
        _isGameOver = true;
    }

    #endregion

}
