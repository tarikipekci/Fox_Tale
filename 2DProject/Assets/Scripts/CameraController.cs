using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Components")] public Transform target;
    public Transform farBackground;
    public Transform middleBackground;

    [Header("Variables")] private float lastXPos;
    private float lastYPos;
    public bool stopFollow;

    private Vector3 endPosition;
    public float desiredDuration;
    private float elapsedTime;

    [Header("Scripts")] public static CameraController instance;


    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        if (stopFollow) return;
        var transform2 = transform;
        var position2 = transform2.position;
        var position1 = position2;
        var amountToMoveX = position1.x - lastXPos;
        var amountToMoveY = position1.y - lastYPos;

        farBackground.position = farBackground.position + new Vector3(amountToMoveX, amountToMoveY, 0f);

        middleBackground.position += new Vector3(amountToMoveX * 0.65f, 0.65f * 0, 0f);

        lastXPos = position2.x;
        lastYPos = position2.y;
    }
}