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

        [Header("Create Username Settings")]
        #region Serialized Fields

        [SerializeField] private CanvasGroup _setUsernamePanel;
        [SerializeField] private TMP_InputField _nameInput;

        #endregion

        #region Awake

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

        #endregion

        #region Private Methods

        private void Login()
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = "New User",
                CreateAccount = true,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetPlayerProfile = true
                }
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
        }

        private void OnLoginSuccess(LoginResult result)
        {
            string name = null;

            if (result.InfoResultPayload.PlayerProfile != null)
                name = result.InfoResultPayload.PlayerProfile.DisplayName;

            if (name == null)
            {
                UIManager.Instance.ActivateCanvasGroup(_setUsernamePanel, true);
            }
            else
            {
                UIManager.Instance.ActivateCanvasGroup(_setUsernamePanel, false);
            }


            GetLeaderboard();
            Debug.Log($"Successful login/account create");
            Debug.Log($"Welcome Back {name}");
        }

        private void OnError(PlayFabError error)
        {
            Debug.Log("Error On Backend!");
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
                texts[1].text = item.Profile.DisplayName;
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

                Debug.Log($"Rank: {item.Position}  Name: {item.DisplayName}  Time: {item.StatValue}");
            }
        }

        private void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
        {
            Debug.Log("Username Updated!");
            UIManager.Instance.ActivateCanvasGroup(_setUsernamePanel, false);
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

        public void SubmitNameRequest()
        {
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = _nameInput.text
            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
        }

        #endregion
    }

}

