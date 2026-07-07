using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.TimeZoneInfo;



public class FadeDirector : MonoBehaviour
{
    // フェードの種類
    public enum FadeState
    {
        In,
        Out,
    }

    [SerializeField] private Datas Datas;   // フェードのデータ
    [SerializeField] private Image panel;   // フェード用パネル
    private float m_currentTransition = 0.0f;   // 現在のフェード時間
    private float transitionTime = 0.0f;    // フェードの時間

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // フェード終了確認
    public bool EndFade()
    {
        return m_currentTransition > Datas.SceneTransitionTime;
    }
    

    // フェードの開始
    public void FadeRequest(FadeState fade, string name = "none")
    {
        // 経過時間ゼロに
        m_currentTransition = 0;
        // フェード時間の設定
        transitionTime = Datas.SceneTransitionTime;
        // フェード用オブジェクトの表示
        panel.gameObject.SetActive(true);

        // Inだったら0から
        float start = (fade == FadeState.In) ? 0.0f : 1.0f;
        // Outだったら1から
        float end = (fade == FadeState.In) ? 1.0f : 0.0f;
        // コルーチンの再生
        StartCoroutine(Fade(start, end, name));
    }

    // フェードの実行
    private IEnumerator Fade(float start, float end, string name)
    {
        while (m_currentTransition < transitionTime)
        {
            // なかったら実行しない
            if (!panel)
                yield return null;
            m_currentTransition += Time.deltaTime;
            // 透明度の変更
            var color = panel.color;
            color.a = Mathf.Lerp(start, end, m_currentTransition / transitionTime);
            panel.color = color;
            // 次のフレームへ
            yield return null;
        }

        // シーン名が登録されていたらシーン移動
        if (name != "none")
            SceneManager.LoadScene(name);
        else
            panel.gameObject.SetActive(false);
    }
}
