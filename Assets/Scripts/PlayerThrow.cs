using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerThrow : MonoBehaviour
{
    public GameObject stonePrefab;
    public Transform throwPoint;

    public Sprite idleSprite;
    public Sprite throwSprite;

    public float throwDelay = 0.1f;   // 投げる動作から石が出るまで
    public float stoneSpeed = 8f;     // 石の速度
    private SpriteRenderer spriteRenderer;

    private bool ThrowingEnd = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && !ThrowingEnd)
        {
            StartCoroutine(ThrowAnimation());
        }
    }

    IEnumerator ThrowAnimation()
    {
        ThrowingEnd = true;
        // 投げるポーズ
        spriteRenderer.sprite = throwSprite;

        // 手を振る時間
        yield return new WaitForSeconds(throwDelay);

        // 石生成
        GameObject stone = Instantiate(
            stonePrefab,
            throwPoint.position,
            Quaternion.identity
        );

        // 石を飛ばす
        Rigidbody2D rb = stone.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float direction = transform.localScale.x > 0 ? 1f : -1f; rb.linearVelocity = new Vector2(direction * stoneSpeed, 1.5f);
        }


        // 投げ終わり
        yield return new WaitForSeconds(0.1f);

        // 元に戻す
        spriteRenderer.sprite = idleSprite;
    }
}
