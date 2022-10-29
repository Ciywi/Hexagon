using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    #region Instance

    public static GameManager Instance;

    #endregion

    #region Fields

    [Header("Game Settings")]
    #region Private Fields

    private float _startGameDelay = 0.2f;

    #endregion

    [Header("Color Variables")]
    #region Private Fields

    private Color _white = Color.white;

    #endregion

    #endregion


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

    private void StartGame()
    {
        SceneManager.LoadScene(1);
        AudioManager.Instance.PlayAudio("Game Music");
    }

    public void PlayButton()
    {
        Invoke(nameof(StartGame), _startGameDelay);
        AudioManager.Instance.StopAudio("Menu Music");
    }

    public IEnumerator GameOver()
    {
        Time.timeScale = 0.2f;

        yield return new WaitForSecondsRealtime(1.0f);

        Hexagon.Instance.HexagonColorChangerOnHit(_white);

        yield return new WaitForSecondsRealtime(1.5f);

        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
        AudioManager.Instance.RestartAudio("Game Music");

    }

}
