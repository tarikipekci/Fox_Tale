using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        PlayerHealthController.instance.currentHealth = 0;
        PlayerHealthController.instance.DealDamage(EnemyController.damage);
    }
}
