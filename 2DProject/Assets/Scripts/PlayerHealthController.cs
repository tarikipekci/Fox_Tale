using System.Collections;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDataPersistence
{
    [Header("Variables")] public int currentHealth, maxHealth;
    public float invinsibleLength;
    public float invincibleCounter;

    [Header("Scripts")] public static PlayerHealthController instance;

    [Header("Components")] private SpriteRenderer spriteRenderer;

    [Header("Game Objects")] public GameObject deathEffect;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void LoadData(GameData data)
    {
        this.currentHealth = data.currentHealth;
        UIController.instance.UpdateHealth();
    }

    public void SaveData(ref GameData data)
    {
        data.currentHealth = this.currentHealth;
    }

    private void Update()
    {
        if (!(invincibleCounter > 0)) return;
        invincibleCounter -= Time.deltaTime;

        if (invincibleCounter <= 0)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,
                1f);
        }
    }

    public void DealDamage(int damage)
    {
        if (invincibleCounter <= 0)
        {
            currentHealth -= damage;
            AudioManager.instance.PlaySfx(9);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                PlayerController.instance.gameObject.SetActive(false);
                AudioManager.instance.PlaySfx(8);
                UIController.instance.deathScreen.gameObject.SetActive(true);
                PauseMenu.instance.pauseScreen.gameObject.SetActive(false);
                Instantiate(deathEffect, transform.position, transform.rotation);
            }
            else
            {
                invincibleCounter = invinsibleLength;

                StartCoroutine(Visibility());

                PlayerController.instance.KnockBack();
            }

            UIController.instance.UpdateHealth();
        }
    }

    private IEnumerator Visibility()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,
                0.53f);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,
                1f);
        }
    }

    public void HealPlayer(int healingAmount)
    {
        currentHealth += healingAmount;
        if (currentHealth == maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.UpdateHealth();
    }
}