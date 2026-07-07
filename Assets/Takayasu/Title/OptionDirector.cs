using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionDirector : MonoBehaviour
{
    [SerializeField] private List<Image> images = new();
    [SerializeField] private Vector3 imageMovement;
    [SerializeField] private float moveTime;
    private float m_minPos = 0; // 左端
    private float m_maxPos = 0; // 右端
    private bool m_isMoving = false;

    // 追加: 各画像の最終目的地を記憶しておくための辞書
    private Dictionary<RectTransform, Vector2> m_targetPositions = new();

    void Start()
    {
        if (images.Count > 0)
        {
            // 初期値を1枚目の画像の座標にしておく（バグ防止）
            m_minPos = images[0].GetComponent<RectTransform>().anchoredPosition.x;
            m_maxPos = images[0].GetComponent<RectTransform>().anchoredPosition.x;

            // 画面内にある画像の中で両端を先に調べておく
            foreach (var i in images)
            {
                float x = i.GetComponent<RectTransform>().anchoredPosition.x;
                if (m_minPos > x) m_minPos = x;
                else if (m_maxPos < x) m_maxPos = x;
            }
        }
    }

    void Update()
    {
    }

    // オブジェクトが非アクティブになった（閉じられた）時に呼ばれる
    private void OnDisable()
    {
        // 1. 移動中フラグを強制的にリセットして、次回開いた時のフリーズを防ぐ
        m_isMoving = false;

        // 2. 途中でコルーチンが死んで中途半端な位置に取り残された画像を、目的地へ強制スナップさせる
        foreach (var kvp in m_targetPositions)
        {
            if (kvp.Key != null)
            {
                kvp.Key.anchoredPosition = kvp.Value;
            }
        }
    }

    public void LeftSlide()
    {
        // 移動中だったら何もしない
        if (m_isMoving)
            return;
        m_isMoving = true;
        foreach (var i in images)
        {
            RectTransform rt = i.GetComponent<RectTransform>();

            // 左にはみ出しすぎた画像を、列の一番右の次の位置へワープさせてループさせる
            if (rt.anchoredPosition.x <= m_minPos + 1.0f)
            {
                Vector2 p = rt.anchoredPosition;
                // ワープさせる
                p.x = m_maxPos + imageMovement.x;
                rt.anchoredPosition = p;
            }

            // 移動先の目的地を計算して記録しておく
            Vector2 moveVec = -imageMovement;
            m_targetPositions[rt] = rt.anchoredPosition + moveVec;

            StartCoroutine(ImageMove(rt, moveVec));
        }
    }

    public void RightSlide()
    {
        // 移動中だったら何もしない
        if (m_isMoving)
            return;
        m_isMoving = true;
        foreach (var i in images)
        {
            RectTransform rt = i.GetComponent<RectTransform>();

            // 右にはみ出しすぎた画像を、列の一番左の次の位置へワープさせてループさせる
            if (rt.anchoredPosition.x >= m_maxPos - 1.0f)
            {
                Vector2 p = rt.anchoredPosition;
                // ワープさせる
                p.x = m_minPos - imageMovement.x;
                rt.anchoredPosition = p;
            }

            // 移動先の目的地を計算して記録しておく
            Vector2 moveVec = imageMovement;
            m_targetPositions[rt] = rt.anchoredPosition + moveVec;

            StartCoroutine(ImageMove(rt, moveVec));
        }
    }

    private IEnumerator ImageMove(RectTransform rt, Vector2 movement)
    {
        Vector2 startPos = rt.anchoredPosition;
        float time = 0f;
        while (time < moveTime)
        {
            time += Time.deltaTime;
            rt.anchoredPosition = Easing.EaseOut(time, startPos, movement, moveTime);
            yield return null;
        }

        m_isMoving = false;
        rt.anchoredPosition = startPos + movement; // 最終到達点を明示的に確定
    }
}