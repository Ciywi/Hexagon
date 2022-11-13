using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

namespace Managers
{

    public class PlayFabManager : MonoBehaviour
    {
        #region Instance

        public static PlayFabManager Instance;

        #endregion

        [Header("Leaderboard Settings")]
        #region Serialized Fields

        [SerializeField] private GameObject _rowPrefab;
        [SerializeField] private Transform _scoresPanel;

        [SerializeField] private Color[] _textColors;
        #endregion

        #region Awake and Start

        void Awake()
        {
            Login();

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
            GetLeaderboard();
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

        private void OnLeaderboardGet(GetLeaderboardResult result)
        {
            foreach (Transform item in _scoresPanel)
            {
                Destroy(item.gameObject);
            }

            foreach (var item in result.Leaderboard)
            {
                GameObject newRow = Instantiate(_rowPrefab, _scoresPanel);
                TextMeshProUGUI[] texts = newRow.GetComponentsInChildren<TextMeshProUGUI>();

                texts[0].text = $"{item.Position + 1}.";
                texts[1].text = item.PlayFabId;
                texts[2].text = item.StatValue.ToString();

                if (item.Position == 0)
                {
                    for (int i = 0; i < texts.Length; i++)
                    {
                        texts[i].color = _textColors[0];
                    }
                }
                if (item.Position == 1)
                {
                    for (int i = 0; i < texts.Length; i++)
                    {
                        texts[i].color = _textColors[1];
                    }
                }
                if (item.Position == 2)
                {
                    for (int i = 0; i < texts.Length; i++)
                    {
                        texts[i].color = _textColors[2];
                    }
                }

                Debug.Log($"Rank: {item.Position}  Name: {item.PlayFabId}  Time: {item.StatValue}");
            }
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

        public void GetLeaderboard()
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = "Best Survive Time",
                StartPosition = 0,
                MaxResultsCount = 10
            };
            PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
        }

        #endregion
    }

}

