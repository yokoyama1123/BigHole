using System.Collections;
using UnityEngine;

public class SelectNextScene : MonoBehaviour
{
    [Header("決められた数字と入力した数字")]
    [SerializeField] private SaveData savedata;

    [Header("何秒差なら地面が見えるか")]
    [SerializeField] private int differenceSecond = 2;

    [Header("地面にぶつかるシーンの名前")]
    [SerializeField] private string Scene1;
    [Header("ピッタリのシーンの名前")]
    [SerializeField] private string Scene2;
    [Header("地面は見えてるけどつかないシーンの名前")]
    [SerializeField] private string Scene3;
    [Header("地面が見えないシーンの名前")]
    [SerializeField] private string Scene4;

    private int m_setSecond;
    private int m_inputSecond;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_setSecond = savedata.SetSecond;
        m_inputSecond = savedata.InputSecond;

        // コルーチンを開始
        StartCoroutine(DelayedGoScene());
    }

    IEnumerator DelayedGoScene()
    {
        // 1秒待機
        yield return new WaitForSeconds(1.5f);

        // シーン遷移を実行
        int difference = m_setSecond - m_inputSecond;
        GoScene(difference);
    }

    private void GoScene(int difference)
    {
        if (difference < 0)//地面にぶつかるシーンへ
        {
            FadeManager.Instance.LoadScene(Scene1, 3.0f);
        }
        else if (difference == 0)//ピッタリのシーンへ
        {
            FadeManager.Instance.LoadScene(Scene2, 3.0f);
        }
        else if (difference >= 0 && difference <= differenceSecond)//地面は見えてるけどつかないシーンへ
        {
            FadeManager.Instance.LoadScene(Scene3, 3.0f);
        }
        else if (difference > differenceSecond)//地面が見えないシーンへ
        {
            FadeManager.Instance.LoadScene(Scene4, 3.0f);
        }
    }
 }
