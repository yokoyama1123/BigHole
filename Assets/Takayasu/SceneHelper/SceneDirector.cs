using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SceneDirector : MonoBehaviour
{

    [SerializeField] private FadeDirector fadeDirector;
    private string m_sceneName;

    public void ReqestChangeScene(string name)
    {
        m_sceneName = name;
        fadeDirector.FadeRequest(FadeDirector.FadeState.In, name);
    }

    
}
