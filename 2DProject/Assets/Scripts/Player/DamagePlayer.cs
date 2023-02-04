using System;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Trap"))
        {
            PlayerHealthController.instance.DealDamage(PlayerController.instance.damage);
        }

        if (gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerHealthController.instance.DealDamage(EnemyController.damage);
            }
        }

        if (gameObject.CompareTag("Bullet"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerHealthController.instance.DealDamage(Bullet.instance.arrowDamage);
            }
        }

        if (gameObject.CompareTag("Boss"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerHealthController.instance.DealDamage(BossController.instance.bossDamage);
            }
        }
    }
}