using UnityEngine;

[CreateAssetMenu(fileName = "TextEffectDatas", menuName = "Scriptable Objects/TextEffectDatas")]
public class TextEffectDatas : ScriptableObject
{
    public float scaleUp = 2.0f;    // 拡大率
    public float scaleDown = 2.0f;    // 縮小率
    public float effectTransitionTime;  // エフェクトにかかる時間

}
