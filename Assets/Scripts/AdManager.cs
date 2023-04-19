using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Managers
{

    public class AdManager : MonoBehaviour, IUnityAdsListener
    {
        #region Instance

        public static AdManager Instance;

        #endregion

        private GameManager gameManager;

        private bool _testMode = false;
        private string _androidGameId = "5018066";

        private GameObject _gameOverPanel;

        #region Awake and Start

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                Advertisement.AddListener(this);
                Advertisement.Initialize(_androidGameId);
            }
        }

        #endregion

        #region Private Methods

        void IUnityAdsListener.OnUnityAdsDidError(string message)
        {
            Debug.Log($"Unity Ads Error Occurred: {message}");
        }

        void IUnityAdsListener.OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            switch (showResult)
            {
                case ShowResult.Finished:
                    _gameOverPanel = GameObject.Find("Game Over Panel");
                    GUIManager.Instance.StartCoroutine(GUIManager.Instance.ResumeGameCountdown(_gameOverPanel, "ContinueGameAfterAD"));
                    break;
                case ShowResult.Skipped:
                    break;
                case ShowResult.Failed:
                    Debug.LogWarning("Advertisement Failed");
                    break;
            }
        }

        void IUnityAdsListener.OnUnityAdsDidStart(string placementId)
        {
            Debug.Log("Advertisement Started");
        }

        void IUnityAdsListener.OnUnityAdsReady(string placementId)
        {
            Debug.Log("Advertisement Ready");
        }

        #endregion

        #region Public Methods

        public void ShowAdvertisement(GameManager gameManager, string bannerID)
        {
            this.gameManager = gameManager;

            Advertisement.Show(bannerID);
        }

        public void BannerAdvertisement(GameManager gameManager, string bannerID)
        {
            this.gameManager = gameManager;

            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show(bannerID);
        }

        #endregion

    }

}

