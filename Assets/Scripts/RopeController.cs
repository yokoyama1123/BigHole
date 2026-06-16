using UnityEngine;
using UnityEngine.InputSystem;

public class RopeController : MonoBehaviour
{
    public DistanceJoint2D rope;
    public float ropeSpeed = 5f;

    void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.eKey.isPressed)
            {
                rope.distance += ropeSpeed * Time.deltaTime;
            }

            if (Keyboard.current.qKey.isPressed)
            {
                rope.distance -= ropeSpeed * Time.deltaTime;
            }
        }

        rope.distance = Mathf.Clamp(rope.distance, 1f, 20f);
    }
}