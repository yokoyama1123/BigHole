using UnityEngine;

public class DefaultScene : MonoBehaviour
{
    [SerializeField] protected SceneDirector sceneDirector;
    [SerializeField] protected TextEffect textEffecter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        // 開始はフェードイン
        FanctionLib.FadeOut();
    }

}
