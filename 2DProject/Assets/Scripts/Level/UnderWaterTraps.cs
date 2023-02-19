using Player;
using UnityEngine;

namespace Level
{
    public class UnderWaterTraps : MonoBehaviour
    {
        [Header("Objects")] public GameObject deathEffect;

        // ReSharper disable once InconsistentNaming
        [Header("Script")] public static UnderWaterTraps instance;

        private void Awake()
        {
            instance = this;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(gameObject);
            Instantiate(deathEffect, gameObject.transform.position, gameObject.transform.rotation);

            if (!gameObject.CompareTag("OceanTrap")) return;
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerHealthController.instance.DealDamage(1);
            }
        }
    }
}