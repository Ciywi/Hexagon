using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    [Header("Earning Point Settings")]
    #region Serialized Fields

    [SerializeField] private int _pointAmount = 10;

    #endregion


    private void OnTriggerExit2D(Collider2D collision)
    {
        GetPoint();
    }

    void GetPoint()
    {
        PointManager.Instance.Point += _pointAmount;
    }
}
