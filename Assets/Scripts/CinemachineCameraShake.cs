using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineCameraShake : MonoBehaviour
{
    #region Instance

    public static CinemachineCameraShake Instance;

    #endregion

    #region Fields

    [Header("Components")]
    #region Private Fields

    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    #endregion

    [Header("ShakeSettings")]
    #region Private Fields

    private float _shakeTimer;

    #endregion

    #endregion

    #region Awake and Start

    void Awake()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion


    #region Update and FixedUpdate

    void Update()
    {
        StopCameraShake();
    }


    #endregion 


    #region Private Methods

    private void StopCameraShake()
    {
        if (_shakeTimer > 0f)
        {
            _shakeTimer -= Time.deltaTime;

            if (_shakeTimer <= 0f)
            {
                ShakeCamera(0f, 0f);
            }
        }
    }

    #endregion


    #region Public Methods

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin 
            = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        _shakeTimer = time;
    }

    #endregion
}
