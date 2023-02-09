using UnityEngine;

namespace Level
{
    public class CameraController : MonoBehaviour
    {
        [Header("Components")] public Transform target;
        public Transform farBackground;
        public Transform middleBackground;

        [Header("Variables")] private float _lastXPos;
        private float _lastYPos;
        public bool stopFollow;

        private Vector3 _endPosition;
        public float desiredDuration;
        private float _elapsedTime;

        // ReSharper disable once InconsistentNaming
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
            var amountToMoveX = position1.x - _lastXPos;
            var amountToMoveY = position1.y - _lastYPos;

            farBackground.position = farBackground.position + new Vector3(amountToMoveX, amountToMoveY, 0f);

            middleBackground.position += new Vector3(amountToMoveX * 0.65f, 0.65f * 0, 0f);

            _lastXPos = position2.x;
            _lastYPos = position2.y;
        }
    }
}