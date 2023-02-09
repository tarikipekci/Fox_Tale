using Player;
using UnityEngine;

namespace Level
{
    public class CheckPointController : MonoBehaviour
    {
        [Header("Scripts")]
        // ReSharper disable once InconsistentNaming
        public static CheckPointController instance;
        private CheckPoint[] _checkPoints;
    
        [Header("Vectors")]
        public Vector3 spawnPoint;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            _checkPoints = FindObjectsOfType<CheckPoint>();
            spawnPoint = PlayerController.instance.transform.position;
        }
    
        public void DeactivateCheckPoints()
        {
            foreach (var t in _checkPoints)
            {
                t.ResetCheckPoint();
            }
        }

        public void SetSpawnPoint(Vector3 newSpawnPoint)
        {
            spawnPoint = newSpawnPoint;
        }
    }
}