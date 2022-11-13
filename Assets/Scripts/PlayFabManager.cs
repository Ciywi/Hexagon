using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

namespace Managers
{

    public class PlayFabManager : MonoBehaviour
    {
        #region Instance

        public static PlayFabManager Instance;

        #endregion


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
            }


        }

        void Start()
        {
            Login();
        }

        #endregion

        #region Private Methods

        private void Login()
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
        }

        private void OnSuccess(LoginResult result)
        {
            Debug.Log("Successful login/account create!");
        }

        private void OnError(PlayFabError error)
        {
            Debug.Log("On Backend!");
            Debug.Log(error.GenerateErrorReport());
        }

        private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
        {
            Debug.Log("Succesfull leaderboard sent");
        }

        #endregion

        #region Public Methods

        public void SendLeaderboard(int time)
        {
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate
                    {
                        StatisticName = "Best Survive Time",
                        Value = time
                    }
                }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
        }

        #endregion
    }

}

