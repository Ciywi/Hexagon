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
    private bool _isGameStarted = false;
    private bool _isGameOver = false;
    private bool _isPaused = false;

    #endregion

    #region Properties

    public bool IsPaused { get { return _isPaused; } set { _isPaused = value; } }

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


    [Header("Music Speed Settings")]
    #region Private Fields

    float _musicTime = 0;

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

        GetPersonalRecord();
    }

    #endregion

    private void Update()
    {
        SpeedUpTheAudio();

        /// Activate On Windows Build
        if (Input.GetKeyDown(KeyCode.Escape) && _isGameOver == false)
        {
            if (_isPaused == false)
            {
                PauseGame();
            }
            else if (_isPaused == true)
            {
                ResumeGame();
            }
        }

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
        _isGameStarted = true;
    }

    #endregion
    private void RestartGame()
    {
        GUIManager.Instance.YourNewBestText.alpha = 0;
        GUIManager.Instance.ActivateCanvasGroup(GUIManager.Instance.GameOverPanel, false);
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
        ShrinkSpeed = _startingShrinkSpeed;
        AudioManager.Instance.RestartAudio("Game Music");
        _isGameOver = false;
    }

    private void SpeedUpTheAudio()
    {
        if (_isGameStarted == false || _isPaused == true)
            return;

        _musicTime += Time.deltaTime;

        if (_musicTime >= 195f)
        {
            AudioManager.Instance.IncrementAudioPitch("Game Music", 0.1f);
            _musicTime = 95;
        }
    }

    private void GetPersonalRecord()
    {
        _bestTime = PlayerPrefs.GetFloat("BestTime", _bestTime);
    }

    #region Public Methods

    public void PlayButton()
    {
        Invoke(nameof(StartGame), _startGameDelay);
        AudioManager.Instance.StopAudio("Menu Music");
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        _isPaused = true;
        GUIManager.Instance.ActivateCanvasGroup(GUIManager.Instance.PauseMenuPanel, true);
        AudioManager.Instance.PauseAudio("Game Music");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        _isPaused = false;
        GUIManager.Instance.ActivateCanvasGroup(GUIManager.Instance.PauseMenuPanel, false);
        AudioManager.Instance.PlayAudio("Game Music");
    }

    public void ExitGame()
    {
        Application.Quit();
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
            PlayerPrefs.SetFloat("BestTime", _bestTime);
            GUIManager.Instance.YourNewBestText.alpha = 1;
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
        GUIManager.Instance.ActivateCanvasGroup(GUIManager.Instance.GameOverPanel, true);
        _isGameOver = true;
    }

    #endregion

}
