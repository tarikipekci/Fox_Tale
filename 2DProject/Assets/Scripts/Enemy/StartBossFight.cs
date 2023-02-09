using Player;
using UnityEngine;

namespace Enemy
{
    public class StartBossFight : MonoBehaviour
    {
        [Header("Game Objects")] public GameObject block;
        public GameObject entrance;
        public GameObject block2;
        public GameObject healthBarOfBoss;

        // ReSharper disable once InconsistentNaming
        [Header("Scripts")] public static StartBossFight instance;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            // ReSharper disable once InvertIf
            if (PlayerHealthController.instance.currentHealth <= 0)
            {
                block.SetActive(false);
                BossController.instance.hitPoints = BossController.instance.maxHitPoints;
                BossController.instance.slider.value = 30;
                BossController.instance.healthLevel.text = BossController.instance.hitPoints + "/30";
                healthBarOfBoss.SetActive(false);
                BossController.instance.waitToTeleport = 5.85f;

                switch (BossController.instance.hitPoints)
                {
                    case <= 0:
                        healthBarOfBoss.gameObject.SetActive(false);
                        block.gameObject.SetActive(false);
                        block2.gameObject.SetActive(false);
                        break;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            healthBarOfBoss.gameObject.SetActive(true);
            block.gameObject.SetActive(true);
        }
    }
}