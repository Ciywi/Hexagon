using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Movement Settings")]
    #region Serialize Field

    [SerializeField][Range(1.0f,750.0f)] private float _moveSpeed = 500.0f;

    #endregion

    #region Private Fields

    private float _xInput;

    #endregion
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        RotateAroundObject(ReferenceManager.Instance.CenterHexagon, _xInput, _moveSpeed);
    }

    private void GetInput()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
    }

    private void RotateAroundObject(GameObject objectToRotateAround, float xInput, float moveSpeed)
    {
        transform.RotateAround(objectToRotateAround.transform.position, Vector3.forward, xInput * Time.fixedDeltaTime * -moveSpeed);
    }
}
