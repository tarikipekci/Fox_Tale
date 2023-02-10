using System.Collections;
using Enemy;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
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

        private float _leftDistance, _rightDistance;
        public int counter;
        public int stompCounter;
        private int _jumpingCounter;
        private int _dashCounter;
        private float _waitForGemSpeech = 3f;
        private float _waitForHealthSpeech = 10f;
        private float _waitForFinalSpeech = 7f;

        // ReSharper disable once InconsistentNaming
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
            _leftDistance = Vector2.Distance(frogLeft.transform.position, player.transform.position);
            _rightDistance = Vector2.Distance(frogRight.transform.position, player.transform.position);

            if (_jumpingCounter < 2)
            {
                PlayerController.instance.canDoubleJump = false;
            }

            StartCoroutine(ChangeSpeechBubble());

            frogSensei.flipX = !(_leftDistance < _rightDistance);

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
                _jumpingCounter++;
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

            if (counter > 0)
            {
                if (speechSeven == 0)
                {
                    spOfSb.sprite = speeches[7];
                    speechSeven++;
                    enemyFrog.SetActive(true);
                    Instantiate(EnemyController1.instance.deathEffect, enemyFrog.transform.position, enemyFrog.transform.rotation);
                }
            }

            if (stompCounter > 0)
            {
                if (speechSix == 0)
                {
                    spOfSb.sprite = speeches[8];
                    speechSix++;
                }
            }

            if (speechSix == 1)
            {
                _waitForGemSpeech -= Time.deltaTime;
            }

            if (speechSix == 1 && _waitForGemSpeech <= 0)
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
                _waitForHealthSpeech -= Time.deltaTime;
            }

            if (speechEight == 1 && _waitForHealthSpeech <= 0)
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
                _waitForFinalSpeech -= Time.deltaTime;
            }

            if (speechTen == 0 && _waitForFinalSpeech <= 0)
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
}