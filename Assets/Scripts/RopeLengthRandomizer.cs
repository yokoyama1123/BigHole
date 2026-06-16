using UnityEngine;
using UnityEngine.InputSystem;

public class RopeLengthRandomizer : MonoBehaviour
{
    public DistanceJoint2D ropeJoint;

    public float minLength = 2f;
    public float maxLength = 8f;

    void Start()
    {
        RandomizeLength();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            RandomizeLength();
        }
    }

    public void RandomizeLength()
    {
        float randomLength = Random.Range(minLength, maxLength);
        ropeJoint.distance = randomLength;

        Debug.Log("縄の長さ: " + randomLength);
    }
}