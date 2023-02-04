using UnityEngine;
using Random = UnityEngine.Random;

public class BatController : MonoBehaviour
{
    [Header("Variables")] public float flyingSpeed;
    public float hitPoints;
    public float maxHitPoints;
    [Range(0f, 100f)] public float chanceToDrop;

    [Header("Components")] public SpriteRenderer sp;

    [Header("Game Objects")] public GameObject stompBox;
    public GameObject deathEffect;
    public GameObject collectible;

    private void Update()
    {
        if (PlayerController.instance.transform.position.x >
            StartBossFight.instance.entrance.transform.position
                .x) // to detect whether the player has entered the boss area or not.
        {
            transform.position = Vector2.MoveTowards(transform.position,
                BossController.instance.target.transform.position, flyingSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hitPoints--;

            if (hitPoints <= 0)
            {
                gameObject.SetActive(false);
                Instantiate(deathEffect, other.transform.position, other.transform.rotation);
                float dropSelect = Random.Range(0f, 100f);

                if (dropSelect <= chanceToDrop)
                {
                    Instantiate(collectible, other.transform.position, other.transform.rotation);
                }

                AudioManager.instance.PlaySfx(3);
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("StompBox"))
        {
            if (stompBox.transform.position.y > gameObject.transform.position.y)
            {
                hitPoints = 0;
                gameObject.SetActive(false);
                LevelManager.gemsCollected++;
                UIController.instance.UpdateGemCount();
                Instantiate(deathEffect, transform.position, transform.rotation);
                PlayerController.instance.Bounce();
                AudioManager.instance.PlaySfx(3);
            }
        }
    }
}