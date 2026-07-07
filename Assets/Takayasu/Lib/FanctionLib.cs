using System.Collections;
using UnityEngine;

// 汎用関数をまとめたクラス
public class FanctionLib : MonoBehaviour
{
    // フェードディレクター
    [SerializeField] private FadeDirector fadeDirector;

    // シングルトン
    public static FanctionLib Instance { get; private set; }

    private void Awake()
    { 
        Instance = this;
    }

    // フェードイン
    static public void FadeIn()
    {
        Instance.fadeDirector.FadeRequest(FadeDirector.FadeState.In);
    }
    // フェードアウト
    static public void FadeOut()
    {
        Instance.fadeDirector.FadeRequest(FadeDirector.FadeState.Out);
    }
    // フェード中
    static public bool IsFade()
    {
        return !Instance.fadeDirector.EndFade();
    }

    // ゲーム終了
    static public void EndGame()
    {
        if(Instance != null)
        {
            Instance.StartCoroutine(End());
        }
    }

    private static IEnumerator End()
    {
        FadeIn();
        while(IsFade())
        {
            yield return null;
        }
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
        #else
                    Application.Quit();//ゲームプレイ終了
        #endif
    }

}
