using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    public float[] m_audioSpectrum;
    public static float spectrumValue { get; private set; }

    #region Start

    private void Start()
    {
        m_audioSpectrum = new float[128]; 
    }

    #endregion

    #region Update

    private void Update()
    {
        PullDataFromAudioListener();
        ExtractBeatFromMusic();
    }

    #endregion

    private void PullDataFromAudioListener()
    {
        AudioListener.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);
    }

    private void ExtractBeatFromMusic()
    {
        if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            spectrumValue = m_audioSpectrum[0] * 100;
        }
    }
}
