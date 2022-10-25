using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HexagonTrigger : MonoBehaviour
{
    private Color _red = Color.red;
    private Color _white = Color.white;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HexagonColorChangerOnHit(_red);
            StartCoroutine("GameOver");
        }
    }

    IEnumerator GameOver()
    {
        Time.timeScale = 0.3f;
        AudioManager.Instance.LowerAudioPitch(0, 1.0f, 0.7f, 0.1f);

        yield return new WaitForSecondsRealtime(1.0f);

        HexagonColorChangerOnHit(_white);

        yield return new WaitForSecondsRealtime(2.0f);

        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    void HexagonColorChangerOnHit(Color colorToBe)
    {
        Hexagon.Instance.HexagonLineRenderer.startColor = colorToBe;
        Hexagon.Instance.HexagonLineRenderer.endColor = colorToBe;
    }
}
