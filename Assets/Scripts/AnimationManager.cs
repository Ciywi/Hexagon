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

    public void ScaleUpAndDownAnimation(Transform objectTransform, Vector3 endScale, float animationDuration, int animationLoopAmount)
    { 
        objectTransform.DOScale(endScale, animationDuration).SetLoops(animationLoopAmount, LoopType.Yoyo);
    }

    public void GlowAnimation(GameObject gameobject, float animationDuration, int animationLoopAmount)
    {
        SpriteRenderer objectRenderer = gameobject.GetComponent<SpriteRenderer>();
        objectRenderer.DOColor(_glow, animationDuration).SetLoops(animationLoopAmount, LoopType.Yoyo);
    }

    public void ScaleUpAnimation(Transform objectTransform, Vector3 startingScale, Vector3 endScale, float animationDuration)
    {
        objectTransform.localScale = startingScale;
        objectTransform.DOScale(endScale, animationDuration).SetUpdate(true);
    }

}
