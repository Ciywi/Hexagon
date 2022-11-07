using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    #region Instance

    public static AnimationManager Instance;

    #endregion

    #region Fields

    [Header("Scale Up And Down Animation Settings")]
    #region Serialized Fields

    [SerializeField][Range(0.1f, 1.5f)] private float _endScale;
    [SerializeField][Range(0.025f, 0.2f)] private float _animatonDuration;
    [SerializeField][Range(2, 10)] private int _animationLoopAmount;

    #endregion

    [Header("Color Animation Variables")]
    #region Serialized Fields

    [SerializeField] private Color _glow;

    #endregion

    #endregion


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ScaleUpAndDownAnimation(GameObject gameobject)
    {
        SpriteRenderer objectRenderer =  gameobject.GetComponent<SpriteRenderer>();
        gameobject.transform.DOScale(_endScale, _animatonDuration).SetLoops(_animationLoopAmount, LoopType.Yoyo);
        objectRenderer.DOColor(_glow, _animatonDuration).SetLoops(_animationLoopAmount, LoopType.Yoyo);
    }
}
