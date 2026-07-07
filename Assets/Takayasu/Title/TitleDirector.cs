using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;
using static UnityEngine.Rendering.DebugUI;

public class TitleDirector : DefaultScene
{
    [SerializeField] private float timeScale = 0;   // 移動時間
    [SerializeField] private Canvas optionCanvas;   // オプションのキャンバス
    private float m_currentTime;// 経過時間
    private bool m_isOption;    // オプション中か
    private Vector2 m_targtePos;
    private Vector2 m_initPos;

    protected override void Start()
    {
        base.Start();
        optionCanvas.gameObject.SetActive(false);
    }

    // 移動の実行
    private IEnumerator Option(Vector2 end, bool isActive = true)
    {
        m_currentTime = 0;
        m_initPos = optionCanvas.gameObject.GetComponent<RectTransform>().anchoredPosition;
        m_targtePos = end;
        while (m_currentTime < timeScale)
        {
            // なかったら実行しない
            if (!optionCanvas)
                yield return null;
            m_currentTime = Mathf.Min(m_currentTime + Time.deltaTime, timeScale);
            // 移動
            optionCanvas.gameObject.GetComponent<RectTransform>().anchoredPosition = Easing.EaseIn(m_currentTime, m_initPos, m_targtePos - m_initPos, timeScale);
            // 次のフレームへ
            yield return null;
        }
        optionCanvas.gameObject.GetComponent<RectTransform>().anchoredPosition = end;
        optionCanvas.gameObject.SetActive(isActive);
    }

    public void OptionOn()
    {
        StartCoroutine(Option(new(0.0f,0.0f)));
        optionCanvas.gameObject.SetActive(true);
        m_isOption = true;
    }
    public void OptionOff()
    {
        StartCoroutine(Option(new(0.0f, 116.0f), false));
        m_isOption = false;
    }

    public void GameEnd()
    {
        if(!m_isOption)
            FanctionLib.EndGame();
    }
}
