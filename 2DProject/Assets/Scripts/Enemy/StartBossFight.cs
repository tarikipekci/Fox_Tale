using UnityEngine;

public class StartBossFight : MonoBehaviour
{
    [Header("Game Objects")] public GameObject block;
    public GameObject entrance;
    public GameObject block2;
    public GameObject healthBarOfBoss;

    [Header("Scripts")] public static StartBossFight instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (PlayerHealthController.instance.currentHealth <= 0)
        {
            block.SetActive(false);
            BossController.instance.hitPoints = BossController.instance.maxHitPoints;
            BossController.instance.slider.value = 30;
            BossController.instance.healthLevel.text = BossController.instance.hitPoints + "/30";
            healthBarOfBoss.SetActive(false);
            BossController.instance.waitToTeleport = 5.85f;

            if (BossController.instance.hitPoints <= 0)
            {
                healthBarOfBoss.gameObject.SetActive(false);
                block.gameObject.SetActive(false);
                block2.gameObject.SetActive(false);
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