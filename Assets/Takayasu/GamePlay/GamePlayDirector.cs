using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GamePlayDirector : DefaultScene
{
    // ゲームプレイシーンの状態
    enum GamePlayState
    { 
        Idle,   // プレイヤーの入力待機
        Action, // プレイヤーの行動中
        Wait,   // 入力不可
    }

    [SerializeField] private TextMeshProUGUI midasi;
    private GamePlayState m_state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        m_state = GamePlayState.Wait;
    }

    // Update is called once per frame
    void Update()
    {
        // 条件が達成されたら次のシーンへ(今はスペース)
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
            sceneDirector.ReqestChangeScene("Result");

        // フェード終了まで何もしない
        if(FanctionLib.IsFade())
            return;

        switch (m_state)
        {
            case GamePlayState.Idle:
                break;
            case GamePlayState.Action:
                break;

            case GamePlayState.Wait:
                if (textEffecter.GetState() == TextEffect.AnimState.Idle)
                {
                    textEffecter.EffectRequst(midasi, TextEffect.EffectPattern.ScaleUp);
                }
                else if(textEffecter.GetState() == TextEffect.AnimState.End)
                {
                    m_state = GamePlayState.Idle;
                }

                break;
            default:
                break;
        }
    }

}
