using System.Collections;
using Enemy;
using Level;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [Header("Game Objects")] public GameObject target;
    public GameObject bat;
    public GameObject deathEffect;
    public GameObject block, block1,block2;
    public GameObject arena;

    [Header("Components")] public Transform left;
    public Transform right;
    public SpriteRenderer sp;
    public Animator anim;
    public Slider slider;
    public Text healthLevel;
    public Transform startingPos;

    [Header("Variables")] public float waitToTeleport;
    public int bossDamage;
    public float spawnBatCounter;
    public float spawnBatDuration;
    public float hitPoints;
    public float maxHitPoints;

    [Header("Scripts")] public static BossController instance;

    private void Awake()
    {
        instance = this;
        spawnBatCounter = spawnBatDuration;
        hitPoints = maxHitPoints;
        anim = GetComponent<Animator>();
        startingPos = gameObject.transform;
    }

    private void Update()
    {
        if (PlayerController.instance.transform.position.x >
            StartBossFight.instance.entrance.transform.position
                .x)                                                                                                     //to detect whether the player has entered the boss area or not.
        {
            anim.SetBool($"isMoving", true);
            if (spawnBatCounter > 0)
            {
                spawnBatCounter -= Time.deltaTime;
            }

            if (spawnBatCounter <= 0)
            {
                var position2 = transform.position;
                bat.transform.position = position2;
                bat.gameObject.SetActive(true);
                Instantiate(bat, position2, transform.rotation);
                spawnBatCounter = spawnBatDuration;
            }

            var position = target.transform.position;
            var leftDistance =
                Vector2.Distance(left.transform.position,
                    position);                                                                                  //to detect location of player for boss is it right of boss or left? 
            var rightDistance = Vector2.Distance(right.transform.position, position);

            if (leftDistance < rightDistance)
            {
                sp.flipX = false;
            }
            else if (rightDistance < leftDistance)
            {
                sp.flipX = true;
            }

            if (waitToTeleport > 0)
            {
                waitToTeleport -= Time.deltaTime;
            }

            float xAxis;
            if (waitToTeleport <= 0)
            {
                var position1 = target.transform.position.x;
                var flipX = sp.flipX;
                if (PlayerController.instance.rb.velocity.x < 0 && !flipX)
                    xAxis = position1 + 1;
                else if (PlayerController.instance.rb.velocity.x > 0 && flipX)
                    xAxis = position1 - 1;
                else if (PlayerController.instance.rb.velocity.x == 0)
                    xAxis = position1;
                else
                    xAxis = target.transform.position.x;

                gameObject.transform.position = new Vector3(xAxis, transform.position.y, 0f);
                waitToTeleport = 5.85f;
            }
        }
        else
        {
            anim.SetBool($"isMoving", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Bullet")) return;
        hitPoints--;
        slider.value--;
        healthLevel.text = hitPoints + "/30";

        if (other.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(ChangeColor());
        }

        if (!(hitPoints <= 0)) return;
        gameObject.SetActive(false);
        LevelManager.GemsCollected += 15;
        Destroy(block);
        Destroy(block1);
        Destroy(block2);
        Destroy(arena);
        StartBossFight.instance.healthBarOfBoss.SetActive(false);
        Instantiate(deathEffect, other.transform.position, other.transform.rotation);
        AudioManager.instance.PlaySfx(3);
    }

    private IEnumerator ChangeColor()                                                                                      //making hit effect by changing colours of the boss object
    {
        sp.color = new Color(0.7f, 0.7f, 0.7f, 1);
        yield return new WaitForSeconds(0.3f);
        sp.color = new Color(1f, 1f, 1f, 1f);
    }
}