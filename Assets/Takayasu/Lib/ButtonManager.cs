using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonSet
    {
        public Button button;
        public Key key;
    }

    [SerializeField] private List<ButtonSet> buttons = new();

    // Update is called once per frame
    void Update()
    {
        foreach (var button in buttons)
        {
            // キーボード接続がうまくいってるかどうか
            if (Keyboard.current == null)
                return;
            // 対象のキーが入力されたらメソッド実行
            if (Keyboard.current[button.key].wasPressedThisFrame)
            {
                button.button.onClick.Invoke();
            }
        }
    }
}
