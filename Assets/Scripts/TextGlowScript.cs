using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TheraBytes.BetterUi;
using DG.Tweening;

public class TextGlowScript : MonoBehaviour
{
    private BetterTextMeshProUGUI _gameText;

    #region Awake and Start

    void Awake()
    {
        _gameText = GetComponent<BetterTextMeshProUGUI>();
    }

    #endregion

    private void Start()
    {
        _gameText.fontSharedMaterial.DOFloat(1.0f, "_GlowPower", 1.0f).SetLoops(-1, LoopType.Restart);
                
    }
}
