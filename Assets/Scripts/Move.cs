using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    #region Fields

    [Header("Movement Settings")]
    #region Serialize Field

    [SerializeField][Range(1.0f,750.0f)] private float _moveSpeed = 550.0f;

    #endregion

    #region Private Fields

    // Windows Input
    // private float _xInput;

    // Mobile Input
    private int _moveInput;

    #endregion

    #endregion

    private void FixedUpdate()
    {
        RotateAroundObject(ReferenceManager.Instance.CenterHexagon, _moveInput, _moveSpeed);
    }

    public void GetInput(int movementDirection)
    {
        _moveInput = movementDirection;
    }

    private void RotateAroundObject(GameObject objectToRotateAround, float xInput, float moveSpeed)
    {
        transform.RotateAround(objectToRotateAround.transform.position, Vector3.forward, xInput * Time.fixedDeltaTime * -moveSpeed);
    }
}
