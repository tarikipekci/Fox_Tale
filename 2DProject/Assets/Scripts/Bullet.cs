using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Variables")] public int arrowDamage;

    [Header("Scripts")] public static Bullet instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Grid") || other.gameObject.CompareTag("Trap") ||
            other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy") ||
            other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}