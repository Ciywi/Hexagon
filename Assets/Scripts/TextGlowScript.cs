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
        _gameText.fontSharedMaterial.SetFloat("_GlowPower", 0.5f);
    }

    #endregion

    private void Start()
    {
        TextGlow(_gameText);
    }

    void TextGlow(BetterTextMeshProUGUI textToGlow)
    {
        textToGlow.fontSharedMaterial.DOFloat(1.0f, "_GlowPower", 1.0f).SetLoops(-1, LoopType.Restart);
    }
}
