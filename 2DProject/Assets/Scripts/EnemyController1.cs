using System.Collections;
using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    [Header("Variables")] public float hitPoints;
    public float maxHitPoints;
    public int stompCounter;
    public int counter;

    [Header("Components")] public SpriteRenderer sp;

    [Header("Game Objects")] public GameObject deathEffect;
    public GameObject stompBox;

    [Header("Scripts")] public static EnemyController1 instance;

    private void Awake()
    {
        instance = this;
        hitPoints = maxHitPoints;
    }

    private IEnumerator ChangeColor()
    {
        sp.color = new Color(sp.color.r, 0f, 0f, sp.color.a);
        yield return new WaitForSeconds(0.1f);
        sp.color = new Color(sp.color.r, 1f, 1f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hitPoints--;

            StartCoroutine(ChangeColor());


            if (hitPoints <= 0)
            {
                gameObject.SetActive(false);
                counter++;
                var transform1 = other.transform;
                Instantiate(deathEffect, transform1.position, transform1.rotation);
                AudioManager.instance.PlaySfx(3);
            }
        }

        if (gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.CompareTag("StompBox"))
            {
                if (stompBox.transform.position.y > gameObject.transform.position.y)
                {
                    PlayerController.instance.Bounce();
                    stompCounter++;
                    gameObject.SetActive(false);
                    var transform1 = other.transform;
                    Instantiate(deathEffect, transform1.position, transform1.rotation);
                    AudioManager.instance.PlaySfx(3);
                }
            }
        }
    }
}