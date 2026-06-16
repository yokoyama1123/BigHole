using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class NumberInput : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    private string currentInput = "";
    private int confirmedNumber = 0; // int型で保存

    void OnEnable()
    {
        if (Keyboard.current != null)
        {
            Keyboard.current.onTextInput += OnTextInput;
        }
    }

    void OnDisable()
    {
        if (Keyboard.current != null)
        {
            Keyboard.current.onTextInput -= OnTextInput;
        }
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // Enterキーが押されたら確定
        if (Keyboard.current.enterKey.wasPressedThisFrame ||
            Keyboard.current.numpadEnterKey.wasPressedThisFrame)
        {
            ConfirmInput();
        }

        // Escapeキーでキャンセル
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CancelInput();
        }
    }

    void OnTextInput(char c)
    {
        if (char.IsDigit(c))
        {
            currentInput += c;
            displayText.text = "入力中: " + currentInput;
        }
        else if (c == '\b') // バックスペース
        {
            if (currentInput.Length > 0)
                currentInput = currentInput.Remove(currentInput.Length - 1);
            displayText.text = "入力中: " + currentInput;
        }
    }

    void ConfirmInput()
    {
        if (!string.IsNullOrEmpty(currentInput))
        {
            // int型に変換して保存
            if (int.TryParse(currentInput, out int number))
            {
                confirmedNumber = number;
                displayText.text = "確定: " + confirmedNumber;
                Debug.Log($"確定した数値: {confirmedNumber} (int型)");

                // 確定後の処理をここに追加
                OnNumberConfirmed(confirmedNumber);
            }
            else
            {
                // 整数に変換できない場合（範囲外など）
                displayText.text = "エラー: 有効な整数ではありません";
                Debug.LogWarning($"変換エラー: {currentInput}");
            }
        }
        else
        {
            displayText.text = "数字が入力されていません";
        }
    }

    void CancelInput()
    {
        currentInput = "";
        displayText.text = "キャンセルしました";
        Debug.Log("入力キャンセル");
    }

    // 確定したint値を取得
    public int GetConfirmedNumber()
    {
        return confirmedNumber;
    }

    // 確定時に呼び出されるコールバック（任意の処理を追加可能）
    void OnNumberConfirmed(int number)
    {
        // 例：数値をゲーム内の別のシステムに渡す
        // GameManager.Instance.SetPlayerNumber(number);
        // またはイベントを発行
        // EventSystem.TriggerNumberConfirmed(number);
    }

    // 入力をリセット
    public void ResetInput()
    {
        currentInput = "";
        confirmedNumber = 0;
        displayText.text = "";
    }
}