using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region Fields

    [Header("Components")]
    #region Serialized Fields

    [SerializeField] private TextMeshProUGUI _timerText;

    #endregion


    [Header("Timer Settings")]
    #region Serialized Fields
    [SerializeField] private bool _isTimerActive;
    [SerializeField][Range(0, 600)] private float _startingTime;
    [SerializeField] private bool _isCountdown;

    #endregion

    #region Private Fields

    private float _currentTime;

    #endregion


    [Header("Limit  Settings")]
    #region Serialized Fields

    [SerializeField][Range(0, 600)] private float _timerLimit;
    [SerializeField] private bool _hasLimit;

    #endregion

    #region Private Fields

    private bool _limitReached;

    #endregion


    [Header("Time Format Settings")]
    #region Serialized Fields
    [SerializeField] private bool _minutesAndSeconds;
    [SerializeField] private TimerFormats _timerFormat;

    #endregion

    #region Private Fields

    private Dictionary<TimerFormats, string> _timeFormatsDictionary = new Dictionary<TimerFormats, string>();

    #endregion

    #region Enum

    private enum TimerFormats { Whole, TenthDecimal }

    #endregion


    #endregion

    #region Start

    void Start()
    {
        _timeFormatsDictionary.Add(TimerFormats.Whole, "0");
        _timeFormatsDictionary.Add(TimerFormats.TenthDecimal, "0.0");
        _currentTime = _startingTime;
    }

    #endregion

    #region Update
    void Update()
    {
        TimerTextColorChanger();
        TimerCountdownOrUp();

        if (_hasLimit)
        {
            TimerLimit();
        }
    }

    void TimerCountdownOrUp()
    {
        if (_isTimerActive)
        {
            _currentTime = _isCountdown ? _currentTime -= Time.deltaTime : _currentTime += Time.deltaTime;

            if (_isCountdown && _currentTime <= 0)
            {
                _currentTime = 0;
                _isTimerActive = false;
            }

            SetTimerText();
        }
    }

    void TimerLimit()
    {
        if ((_isCountdown && _currentTime <= _timerLimit) || (!_isCountdown && _currentTime >= _timerLimit))
        {
            _isTimerActive = false;
            _currentTime = _timerLimit;
            SetTimerText();
        }
    }

    void SetTimerText()
    {
        if (_minutesAndSeconds)
        {
            float minutes = Mathf.FloorToInt(_currentTime / 60);
            _timerText.text = $"{minutes}:{_currentTime % 60:00}";
        }
        else if (!_minutesAndSeconds)
        {
            _timerText.text = _currentTime.ToString(_timeFormatsDictionary[_timerFormat]);
        }
    }

    void TimerTextColorChanger()
    {
        if (_isCountdown)
        {
            if (_currentTime > _startingTime / 2 && _currentTime > 10)
            {
                _timerText.color = Color.green;
            }
            else if (_currentTime <= _startingTime / 2 && _currentTime > 10)
            {
                _timerText.color = Color.yellow;
            }
            else if (_currentTime <= 10)
            {
                _timerText.color = Color.red;
            }
        }
    }

    #endregion
}
