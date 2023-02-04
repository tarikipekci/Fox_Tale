using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class TutorialController : MonoBehaviour
{
    [Header("Game Objects")] public GameObject block, block2;
    public GameObject frogLeft, frogRight;
    public GameObject player;
    public GameObject enemyFrog;
    [FormerlySerializedAs("Gems")] public GameObject gems;
    [FormerlySerializedAs("Healths")] public GameObject healths;

    [Header("Components")] public Sprite[] speeches;
    public Rigidbody2D rb;
    [FormerlySerializedAs("FrogSensei")] public SpriteRenderer frogSensei;
    public SpriteRenderer spOfSb;
    public Rigidbody2D fireBall;

    [Header("Variables")] public int speechOne,
        speechTwo,
        speechThree,
        speechFour,
        speechFive,
        speechSix,
        speechSeven,
        speechEight,
        speechNine,
        speechTen;

    private float leftDistance, rightDistance;

    private int jumpingCounter;
    private int dashCounter;
    private float waitForGemSpeech = 3f;
    private float waitForHealthSpeech = 10f;
    private float waitForFinalSpeech = 7f;

    [Header("Scripts")] public static TutorialController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        spOfSb.sprite = speeches[0];
    }

    private void Update()
    {
        leftDistance = Vector2.Distance(frogLeft.transform.position, player.transform.position);
        rightDistance = Vector2.Distance(frogRight.transform.position, player.transform.position);

        if (jumpingCounter < 2)
        {
            PlayerController.instance.canDoubleJump = false;
        }

        StartCoroutine(ChangeSpeechBubble());

        frogSensei.flipX = !(leftDistance < rightDistance);

        if (speechOne != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (speechThree == 0 && speechOne != 0)
                {
                    spOfSb.sprite = speeches[2];
                    rb.constraints = RigidbodyConstraints2D.None;
                    rb.freezeRotation = true;
                    speechThree++;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && speechThree != 0)
        {
            jumpingCounter++;
        }

        if (PlayerController.instance.counter == 1)
        {
            if (speechTwo == 0 && speechThree != 0)
            {
                spOfSb.sprite = speeches[3];
                speechTwo++;
            }
        }

        if (PlayerController.instance.isDashing)
        {
            if (speechFour == 0 && speechTwo != 0)
            {
                spOfSb.sprite = speeches[4];
                speechFour++;
                Destroy(block);
                fireBall.gravityScale = 5f;
            }
        }

        if (EnemyController1.instance.counter > 0)
        {
            if (speechSeven == 0)
            {
                spOfSb.sprite = speeches[7];
                speechSeven++;
                enemyFrog.SetActive(true);
                Instantiate(EnemyController1.instance.deathEffect, enemyFrog.transform.position, enemyFrog.transform.rotation);
            }
        }

        if (EnemyController1.instance.stompCounter > 0)
        {
            if (speechSix == 0)
            {
                spOfSb.sprite = speeches[8];
                speechSix++;
            }
        }

        if (speechSix == 1)
        {
            waitForGemSpeech -= Time.deltaTime;
        }

        if (speechSix == 1 && waitForGemSpeech <= 0)
        {
            if (speechEight == 0)
            {
                spOfSb.sprite = speeches[9];
                speechEight++;
                gems.SetActive(true);
            }
        }

        if (speechEight == 1)
        {
            waitForHealthSpeech -= Time.deltaTime;
        }

        if (speechEight == 1 && waitForHealthSpeech <= 0)
        {
            if (speechNine == 0)
            {
                PlayerHealthController.instance.currentHealth = 1;
                UIController.instance.UpdateHealth();
                spOfSb.sprite = speeches[10];
                speechNine++;
                healths.SetActive(true);
            }
        }

        if (speechNine == 1)
        {
            waitForFinalSpeech -= Time.deltaTime;
        }

        if (speechTen == 0 && waitForFinalSpeech <= 0)
        {
            spOfSb.sprite = speeches[11];
            speechTen++;
        }

        IEnumerator ChangeSpeechBubble()
        {
            yield return new WaitForSeconds(3.5f);
            if (speechOne == 0)
            {
                spOfSb.sprite = speeches[1];
                speechOne++;
            }
        }
    }
}