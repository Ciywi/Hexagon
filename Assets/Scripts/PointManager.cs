using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    #region Instance

    public static PointManager Instance;

    #endregion
    [Header("Point Settings")]
    #region Serialized Fields

    [SerializeField] private int _point = 0;

    #endregion

    #region Properties

    public int Point { get { return _point; } set { _point = value; } }

    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
