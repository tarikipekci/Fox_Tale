using System;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public LayerMask player;

    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(gameObject.transform.position, 1f,player))
        {
            PlayerHealthController.instance.currentHealth = 0;
            PlayerHealthController.instance.DealDamage(EnemyController.damage);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        PlayerHealthController.instance.currentHealth = 0;
        PlayerHealthController.instance.DealDamage(EnemyController.damage);
    }
}
