
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Grid") || other.gameObject.CompareTag("Trap") ||
            other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy") ||
            other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}