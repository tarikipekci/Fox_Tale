using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    [Header("Scripts")]
    public static CheckPointController instance;
    private CheckPoint[] checkPoints;
    
    [Header("Vectors")]
    public Vector3 spawnPoint;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        checkPoints = FindObjectsOfType<CheckPoint>();
        spawnPoint = PlayerController.instance.transform.position;
    }
    
    public void DeactivateCheckPoints()
    {
        foreach (var t in checkPoints)
        {
            t.ResetCheckPoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}