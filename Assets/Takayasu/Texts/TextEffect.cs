using System;
using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using static Easing;

public class TextEffect : MonoBehaviour
{
    
    // Effectのパターン
    public enum EffectPattern
    { 
        Fade,
        ScaleUp,
        ScaleDown,
    }

    // アニメーションの状態
    public enum AnimState
    {
        Idle,
        Play,
        End,
    }

    // エフェクトのデータ
    [SerializeField] private TextEffectDatas effectData;
    // アニメーションの状態
    AnimState m_animState;


    // 再生中か
    public AnimState GetState()
    {
        return m_animState;
    }

    public void EffectRequst(TextMeshProUGUI text, EffectPattern pattern)
    {
        // 状態を変化
        m_animState = AnimState.Play;
        switch (pattern)
        {
            case EffectPattern.Fade:
                break;
            case EffectPattern.ScaleUp:
                StartCoroutine(ScaleUp(text));
                break;
            case EffectPattern.ScaleDown:
                break;
            default:
                break;
        }
    }

    private IEnumerator ScaleUp(TextMeshProUGUI text)
    {
        // 初期と目標のスケール
        Vector3 initScale = text.GetComponent<RectTransform>().localScale;
        Vector3 targetScale = initScale * effectData.scaleUp;

        float currentTime = 0.0f;
        while(currentTime < effectData.effectTransitionTime)
        {
            currentTime += Time.deltaTime;
            // スケールのチェンジ
            text.GetComponent<RectTransform>().localScale = Easing.EaseOut(currentTime, initScale, targetScale - initScale, effectData.effectTransitionTime);
            yield return null;
        }
        text.transform.localScale = targetScale;
        m_animState = AnimState.End;
    }

}
