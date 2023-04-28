using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    #region Fields

    [Header("Movement Settings")]
    #region Serialize Field

    [SerializeField][Range(1.0f, 750.0f)] private float _moveSpeed = 550.0f;

    #endregion

    #region Private Fields

#if UNITY_STANDALONE_WIN || UNITY_WEBGL
    private float _xInput;
#endif

#if UNITY_ANDROID
    private int _moveInput;
#endif

    #endregion

    #endregion

    #region Update and FixedUpdate


    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
#if UNITY_STANDALONE_WIN || UNITY_WEBGL
        RotateAroundObject(ReferenceManager.Instance.CenterHexagon, _xInput, _moveSpeed);
#endif
#if UNITY_ANDROID
        RotateAroundObject(ReferenceManager.Instance.CenterHexagon, _moveInput, _moveSpeed);
#endif
    }

    #endregion

    #region Private Methods

    private void GetInput()
    {

#if UNITY_STANDALONE_WIN || UNITY_WEBGL
        _xInput = Input.GetAxisRaw("Horizontal");
#endif

    }

#if UNITY_STANDALONE_WIN ||UNITY_WEBGL
    private void RotateAroundObject(GameObject objectToRotateAround, float xInput, float moveSpeed)
    {
        transform.RotateAround(objectToRotateAround.transform.position, Vector3.forward, xInput * Time.fixedDeltaTime * -moveSpeed);
    }
#endif

#if UNITY_ANDROID
    private void RotateAroundObject(GameObject objectToRotateAround, int _moveInput, float moveSpeed)
    {
        transform.RotateAround(objectToRotateAround.transform.position, Vector3.forward, _moveInput * Time.fixedDeltaTime * -moveSpeed);
    }
#endif

    #endregion

    #region Public Methods

#if UNITY_ANDROID
    public void GetInput(int movementDirection)
    {
        _moveInput = movementDirection;
    }
#endif

    #endregion

}
