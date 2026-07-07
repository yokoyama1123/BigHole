using UnityEngine;
using UnityEngine.SceneManagement;

public class GoSceneButton : MonoBehaviour
{
    // シーン名を引数で受け取る（ボタンから直接指定可能）
    public void LoadScene(string sceneName)
    {
        if (sceneName == "Exit")
        {
            QuitGame();
        }
        else
        {
            FadeManager.Instance.LoadScene(sceneName, 0.9f);
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}