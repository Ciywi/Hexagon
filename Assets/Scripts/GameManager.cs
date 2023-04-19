using Managers;
using Nojumpo;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [Header("Pause Settings")]
    #region Private Fields

    private Button _pauseButton;

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

    [Header("Tutorial Panel Settings")]
    #region Private Fields

    private GameObject _tutorialPanel;

    #endregion

    [Header("Game Over Panel Animation Settings")]
    #region Private Fields

    private Transform _objectTransform;

    #endregion

    [Header("Advertisement Settings")]
    #region Serialized Fields

    private Button _watchAdButton;
    public int DieCount { get; set; } = 0;

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



    #region Awake and Start

    private void Awake() {
        GetPersonalRecord();

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

    private void Start() {
        AdManager.Instance.BannerAdvertisement(this, "bannerAndroid");
    }

    #endregion

    private void Update() {
        SpeedUpTheAudio();

#if UNITY_STANDALONE_WIN
        if (Input.GetKeyDown(KeyCode.Escape) && _isGameOver == false)
        {
            if (_isPaused == false)
            {
                PauseGame();
            }
        }

        if (_isGameStarted == false)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartGame();
            }
        }
#endif

    }

    #region Private Methods

    private void SpeedUpTheAudio() {
        if (_isGameStarted == false || _isPaused == true || _isGameOver == true)
            return;

        _musicTime += Time.deltaTime;

        if (_musicTime >= 135.0f)
        {
            AudioManager.Instance.IncrementAudioPitch("Game Music", 0.1f);
            _musicTime = 95.0f;
        }
    }

    private void GetPersonalRecord() {
        _bestTime = PlayerPrefs.GetFloat("BestTime", _bestTime);
    }

    private void SetPersonalRecord() {
        _bestTime = TimeManager.Instance.CurrentTime;
        PlayerPrefs.SetFloat("BestTime", _bestTime);

    }

    private void LoadScene() {
        SceneManager.LoadScene(1);
        Invoke(nameof(OpenTutorialPanel), 0.1f);
    }

    private void OpenTutorialPanel() {
        TutorialPanel tutorialPanel = FindObjectOfType<TutorialPanel>(true);
        _tutorialPanel = tutorialPanel.gameObject;
        _tutorialPanel.SetActive(true);
        Time.timeScale = 0;
    }

    #endregion

    #region Public Methods

    public void StartGame() {
        Time.timeScale = 1;
        _musicTime = 0;
        _tutorialPanel.SetActive(false);
        _startingShrinkSpeed = _shrinkSpeed;
        AudioManager.Instance.PlayAudio("Game Music");
        _isGameStarted = true;
    }

    public void RestartGame() {
        GUIManager.Instance.YourNewBestText.alpha = 0;
        GUIManager.Instance.GameOverPanel.SetActive(false);
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
        ShrinkSpeed = _startingShrinkSpeed;
        AudioManager.Instance.RestartAudio("Game Music");
        _isGameOver = false;
        _musicTime = 0.0f;
    }

    public void PlayButton() {
        Invoke(nameof(LoadScene), _startGameDelay);
        AudioManager.Instance.StopAudio("Menu Music");
    }

    public void PauseGame() {
        Time.timeScale = 0.0f;
        _isPaused = true;
        GUIManager.Instance.PauseMenuPanel.SetActive(true);
        AudioManager.Instance.PauseAudio("Game Music");
    }

    public void ResumeGame() {
        Time.timeScale = 1.0f;
        _isPaused = false;
        AudioManager.Instance.PlayAudio("Game Music");
    }


    public void ExitGame() {
        Application.Quit();
    }
    public void ContinueGameAfterAD() {
        Time.timeScale = 1.0f;
        AudioManager.Instance.SetAudioPitch("Game Music", 1.0f);
        AudioManager.Instance.PlayAudio("Game Music");
        _isGameOver = false;
    }

    public void WatchAdToContinue() {
        AdManager.Instance.ShowAdvertisement(this, "rewardedVideo");

        _watchAdButton = GameObject.Find("Watch Ad Button").GetComponent<Button>();
        _watchAdButton.interactable = false;
    }
    public void ShrinkSpeedUp(float speedUp) {
        ShrinkSpeed += speedUp;
    }


    #endregion

    #region Coroutines

    #region Private
    private IEnumerator PersonalRecordAnimation() {
        yield return new WaitForSecondsRealtime(1.0f);
        GUIManager.Instance.YourNewBestText.alpha = 1;
        AnimationManager.Instance.ScaleUpAnimation(GUIManager.Instance.YourNewBestText.transform, Vector3.zero, Vector3.one, 0.3f);
        AudioManager.Instance.PlayAudio("Congratulations");
    }

    #endregion

    #region Public

    public IEnumerator GameOver() {
        _pauseButton = GameObject.Find("Pause Button").GetComponent<Button>();
        _pauseButton.interactable = false;

        Time.timeScale = 0.2f;

        yield return new WaitForSecondsRealtime(2.0f);

        if (DieCount == 4)
        {
            AdManager.Instance.ShowAdvertisement(this, "interstitialAndroid");
            DieCount = 0;
        }

        Time.timeScale = 0f;
        AudioManager.Instance.PauseAudio("Game Music");
        GUIManager.Instance.GameTimeTextUpdate();
        GUIManager.Instance.GameOverPanel.SetActive(true);
        AudioManager.Instance.PlayAudio("Game Over");
        AnimationManager.Instance.ScaleUpAnimation(GUIManager.Instance.GameOverBorderTransform, Vector3.zero, Vector3.one, 1.0f);

        if (TimeManager.Instance.CurrentTime > _bestTime)
        {
            SetPersonalRecord();
            PlayFabManager.Instance.SendLeaderboard((int)_bestTime);
            StartCoroutine(nameof(PersonalRecordAnimation));
        }

        _isGameOver = true;
        _pauseButton.interactable = true;
    }

    #endregion

    #endregion

}
