using Cinemachine;
using UnityEngine;

public class CinemachineCameraShake : MonoBehaviour
{
    #region Instance

    private static CinemachineCameraShake _instance;

    public static CinemachineCameraShake Instance { get { return _instance; } }

    #endregion

    #region Fields

    [Header("Components")]
    #region Private Fields

    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    #endregion

    [Header("Shake Settings")]
    #region Private Fields

    private float _shakeTimer;

    #endregion

    #endregion

    #region Awake and Start

    private void Awake() {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    #endregion


    #region Update and FixedUpdate

    private void Update() {
        StopCameraShake();
    }


    #endregion 


    #region Private Methods

    private void StopCameraShake() {
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

    public void ShakeCamera(float intensity, float time) {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin
            = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        _shakeTimer = time;
    }

    #endregion
}
