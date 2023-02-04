using UnityEngine;
using UnityEngine.Serialization;

public class CheckPoint : MonoBehaviour
{
    [FormerlySerializedAs("_spriteRenderer")] [Header("Components")] public SpriteRenderer spriteRenderer;
    public Sprite cpOn, cpOff;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (!(CheckPointController.instance.spawnPoint.x < transform.position.x)) return;
        CheckPointController.instance.DeactivateCheckPoints();
        CheckPointController.instance.SetSpawnPoint(transform.position);
        spriteRenderer.sprite = cpOn;
    }

    public void ResetCheckPoint()
    {
        spriteRenderer.sprite = cpOff;
    }
}