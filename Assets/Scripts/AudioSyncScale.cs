using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioSyncScale : AudioSyncer
{
    #region Public Fields

    public Vector3 beatScale;
    public Vector3 restScale;

    #endregion

    #region Public Methods

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (m_isBeat) return;

        transform.localScale = Vector3.Lerp(transform.localScale, restScale, restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        StopCoroutine(nameof(MoveToScale));
        StartCoroutine(nameof(MoveToScale), beatScale);
    }

    #endregion

    #region Coroutines

    private IEnumerator MoveToScale(Vector3 target)
    {
        Vector3 current = transform.localScale;
        Vector3 initial = current;
        float timer = 0;

        while (current != target)
        {
            current = Vector3.Lerp(initial, target, timer / timeToBeat);
            timer += Time.deltaTime;

            transform.localScale = current;

            yield return null;
        }

        m_isBeat = false;
    }

    #endregion

}
