using UnityEngine;

public class FallBackImageMove : MonoBehaviour
{
    // 動かすスピード
    [Header("動かす速度")]
    [SerializeField] private float MOVE_SPEED;

    // 更新処理
    private void Update()
    {
        // 常に上へと画像を動かす
        transform.Translate(0.0f, MOVE_SPEED, 0.0f);
    }
}