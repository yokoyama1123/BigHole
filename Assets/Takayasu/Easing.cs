using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class Easing : MonoBehaviour
{
    public static float Linear(float now, float destination, float distance, float max)
    {
        return distance * now / max + destination;
    }
    public static Vector2 EaseIn(float now, Vector2 destination, Vector2 distance, float max)
    {
        now /= max;
        return distance * now * now + destination;
    }

    public static Vector2 EaseOut(float now, Vector2 destination, Vector2 distance, float max)
    {
        return distance * (-(Mathf.Pow(2.0f, (-20.0f * now / max))) + 1.0f) + destination;
    }

    public static Vector3 EaseOut(float now, Vector3 destination, Vector3 distance, float max)
    {
        return distance * (-(Mathf.Pow(2.0f, (-20.0f * now / max))) + 1.0f) + destination;
    }
}

