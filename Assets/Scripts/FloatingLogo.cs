using UnityEngine;

public class FloatingLogo : MonoBehaviour
{
    public float amplitude = 0.5f;  // 上下の動く幅
    public float speed = 1.0f;      // 動く速さ

    private Vector3 startPos;

    void Start()
    {
        // スタート時の位置を記憶
        startPos = transform.position;
    }

    void Update()
    {
        // サイン波でY軸のオフセットを計算し、位置を更新
        float offsetY = Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(startPos.x, startPos.y + offsetY, startPos.z);
    }
}