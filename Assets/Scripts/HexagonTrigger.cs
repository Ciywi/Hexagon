using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HexagonTrigger : MonoBehaviour
{
    //[SerializeField] private AudioSource _backgroundAudio;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("GameOver");
        }
    }

    IEnumerator GameOver()
    {
        Time.timeScale = 0.3f;
        AudioManager.Instance.LowerAudioPitch();

        //_backgroundAudio.pitch = Mathf.Lerp(1.0f, 0.7f, 0.1f);

        yield return new WaitForSecondsRealtime(3f);

        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
