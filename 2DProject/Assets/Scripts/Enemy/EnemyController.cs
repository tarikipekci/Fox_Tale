using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour, IDataPersistence
{

    [Header("Scripts")] public static EnemyController instance;
    [Header("Variables")] public float moveSpeed;
    private bool movingRight;
    public float moveTime, waitTime;
    private float moveCount, waitCount;
    public int hitPoints;
    public int maxHitPoints;
    public int counter;
    public static int damage;
    [Range(0f, 100f)] public float chanceToDrop;

    [Header("Components")] public Transform leftPoint, rightPoint;
    public Rigidbody2D rb;
    public SpriteRenderer sp;
    private Animator anim;

    [Header("Game Objects")] public GameObject deathEffect;
    public GameObject collectible;
    public GameObject stompBox;
    public EnemyController[] frogs;

    private void Awake()
    {
        instance = this;
        frogs = FindObjectsOfType<EnemyController>();
    }

    private void Start()
    {
        damage = 3;
        hitPoints = maxHitPoints;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        leftPoint.parent = null;
        rightPoint.parent = null;
        movingRight = true;
        moveCount = moveTime;
    }
    
    private void Update()
    {
        anim.SetBool($"isMoving", !(rb.velocity.x <= 0));

        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;
            if (movingRight)
            {
                sp.flipX = true;
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                sp.flipX = false;
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }
            }

            if (moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * 0.75f, waitTime * 1.25f);
            }

            anim.SetBool($"isMoving", true);
        }
        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            rb.velocity = new Vector2(0f, rb.velocity.y);
            if (waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * 0.75f, waitTime * 0.75f);
            }

            anim.SetBool($"isMoving", false);
        }
    }

    private IEnumerator ChangeColor()
    {
        sp.color = new Color(sp.color.r, 0f, 0f, sp.color.a);
        yield return new WaitForSeconds(0.1f);
        sp.color = new Color(sp.color.r, 1f, 1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Bullet")) return;
        hitPoints--;

        if (other.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(ChangeColor());
        }

        if (!(hitPoints <= 0)) return;
        counter++;
        gameObject.SetActive(false);
        Instantiate(deathEffect, other.transform.position, other.transform.rotation);
        AudioManager.instance.PlaySfx(3);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("StompBox")) return;
        if (!(stompBox.transform.position.y > gameObject.transform.position.y)) return;
        hitPoints = 0;
        gameObject.SetActive(false);
        counter++;
        LevelManager.gemsCollected++;
        UIController.instance.UpdateGemCount();
        var transform1 = transform;
        Instantiate(deathEffect, transform1.position, transform1.rotation);
        PlayerController.instance.Bounce();

        var dropSelect = Random.Range(0f, 100f);

        if (dropSelect <= chanceToDrop)
        {
            var transform2 = other.transform;
            Instantiate(collectible, transform2.position, transform2.rotation);
        }
        AudioManager.instance.PlaySfx(3);
    }

    public void LoadData(GameData data)
    {
        for(int i = 0; i <4; i++)
        {
            frogs[i].transform.position = data.frogPos[i];
        }
    }

    public void SaveData(ref GameData data)
    {
        for(int i = 0; i <4; i++)
        {
            data.frogPos[i] = frogs[i].transform.position;
        }
    }
}