using Enemy;
using UnityEngine;

namespace Player
{
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
                    PlayerHealthController.instance.DealDamage(EnemyController.Damage);
                }
            }

            if (gameObject.CompareTag("Bullet"))
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    PlayerHealthController.instance.DealDamage(Bullet.instance.arrowDamage);
                }
            }

            // ReSharper disable once InvertIf
            if (gameObject.CompareTag("Boss"))
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    PlayerHealthController.instance.DealDamage(BossController.instance.bossDamage);
                }
            }
        }
    }
}