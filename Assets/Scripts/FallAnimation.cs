using UnityEngine;

public class FallAnimation : MonoBehaviour
{
    // アニメーター
    private Animator animator;

    // 開始処理
    void Start()
    {
        // フレームレート
        Application.targetFrameRate = 60;
        // アニメーターの取得
        this.animator = GetComponent<Animator>();
        // 音を鳴らす
        GetComponent<AudioSource>().Play();
    }

    // 更新処理
    void Update()
    {
        // アニメーションの再生
        this.animator.Play("FallAnim");
    }
}