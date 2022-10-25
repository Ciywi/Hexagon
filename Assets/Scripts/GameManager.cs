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

    [Header("Components")]
    #region Private Fields

    private GameObject _centerHexagon;

    #endregion

    #region Properties

    public GameObject CenterHexagon { get { return _centerHexagon; } set { _centerHexagon = value; } }

    #endregion


    [Header("Color Variables")]
    #region Private Fields

    private Color _white = Color.white;

    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        _centerHexagon = GameObject.Find("Center Hexagon");
    }

    public IEnumerator GameOver()
    {
        Time.timeScale = 0.2f;

        yield return new WaitForSecondsRealtime(1.0f);

        Hexagon.Instance.HexagonColorChangerOnHit(_white);

        yield return new WaitForSecondsRealtime(1.5f);

        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
