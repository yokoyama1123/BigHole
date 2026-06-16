using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class OnClickUI : MonoBehaviour
{
    [TextArea(3, 5)]
    [SerializeField] private string message = "左クリックされました！";

    [SerializeField] private TextMeshProUGUI messageText;

    private bool EndChange = false;

    void Update()
    {
        // Input Systemでの左クリック検出（シンプル版）
        Mouse mouse = Mouse.current;
        if (mouse != null && mouse.leftButton.wasPressedThisFrame)
        {
            if (messageText != null && !EndChange)
            {
                EndChange = true;
                messageText.text = message;
            }
            Debug.Log(message);
        }
    }
}