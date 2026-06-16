using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public GameObject throwPrefab;   // 投げる物
    public Transform throwPoint;     // 発射位置
    public float throwPower = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Throw();
        }
    }

    void Throw()
    {
        // マウス位置を取得
        Vector3 mousePos =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;

        // 投げる物を生成
        GameObject obj =
            Instantiate(throwPrefab, throwPoint.position, Quaternion.identity);

        // 方向を計算
        Vector2 direction =
            (mousePos - throwPoint.position).normalized;

        // 力を加える
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * throwPower;
    }
}