using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    #region Protected Fields

    protected bool m_isBeat;

    #endregion

    #region Private Fields

    private float m_previousAudioValue;
    private float m_audioValue;
    private float m_timer;

    #endregion

    #region Public Fields

    public float bias;
    public float timeStep;
    public float timeToBeat;
    public float restSmoothTime;

    #endregion

    #region Update

    void Update()
    {
        OnUpdate();
    }

    #endregion 

    #region Public Methods

    public virtual void OnUpdate()
    {
        m_previousAudioValue = m_audioValue;
        m_audioValue = AudioSpectrum.spectrumValue;
        
        if (m_previousAudioValue > bias && m_audioValue <= bias)
        {
            if (m_timer > timeStep)
            {
                OnBeat();
            }
        }

        if (m_previousAudioValue <= bias && m_audioValue > bias)
        {
            if (m_timer > timeStep)
            {
                OnBeat();
            }
        }

        m_timer += Time.deltaTime;
    }

    public virtual void OnBeat()
    {
        m_timer = 0;
        m_isBeat = true;
    }

    #endregion
}
