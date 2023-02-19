using Enemy;
using Player;
using UnityEngine;

namespace Level
{
    public class OceanPartBossController : MonoBehaviour
    {
        [Header("Game Objects")] public GameObject fishBoss;
        public GameObject bossHealthBar;
        public FishBossController boss;

        [Header("Scripts")]
        // ReSharper disable once InconsistentNaming
        public static OceanPartBossController instance;

        [Header("Variables")] public bool enteredBossArea;
        private Vector2 _startingPosition;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (boss.hitPoints <= 0)
            {
                boss.gameObject.SetActive(false);

                if (OceanSidePart2Controller.instance.globalLight.color != Color.white)
                {
                    if (OceanSidePart2Controller.instance.globalLight.intensity <= 5)
                    {
                        OceanSidePart2Controller.instance.globalLight.intensity += 0.005f;
                    }
                }
                PlayerController.instance.rb.velocity = new Vector2(12f, 0f);
                bossHealthBar.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            enteredBossArea = true;
            fishBoss.gameObject.SetActive(true);
            bossHealthBar.SetActive(true);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - 5, transform.position.y,
                transform.position.z);
        }
    }
}