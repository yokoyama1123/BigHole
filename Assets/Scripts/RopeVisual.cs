using UnityEngine;

public class RopeVisual : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        line.positionCount = 2;

        line.SetPosition(0, startPoint.position);
        line.SetPosition(1, endPoint.position);
    }
}
