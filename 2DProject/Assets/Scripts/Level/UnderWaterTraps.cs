using Player;
using UnityEngine;

namespace Level
{
    public class UnderWaterTraps : MonoBehaviour
    {
        [Header("Objects")] public GameObject deathEffect;
        public GameObject bullet;
        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(gameObject);
            Instantiate(deathEffect, gameObject.transform.position, gameObject.transform.rotation);

            if (gameObject.CompareTag("OceanTrap"))
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    PlayerHealthController.instance.DealDamage(PlayerController.instance.damage);
                }

                if (other.gameObject.CompareTag("Bullet"))
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}