using System.Collections;
using Level;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyController : MonoBehaviour, IDataPersistence
    {

        // ReSharper disable once InconsistentNaming
        //[Header("Scripts")] private static EnemyController instance;
        [Header("Variables")] public float moveSpeed;
        private bool _movingRight;
        public float moveTime, waitTime;
        private float _moveCount, _waitCount;
        public int hitPoints;
        public int maxHitPoints;
        public int counter;
        public static int Damage;
        [Range(0f, 100f)] public float chanceToDrop;

        [Header("Components")] public Transform leftPoint, rightPoint;
        public Rigidbody2D rb;
        public SpriteRenderer sp;
        private Animator _anim;

        [Header("Game Objects")] public GameObject deathEffect;
        public GameObject collectible;
        public GameObject stompBox;
        public EnemyController[] frogs;

        private void Awake()
        {
            //instance = this;
            frogs = FindObjectsOfType<EnemyController>();
        }

        private void Start()
        {
            Damage = 3;
            hitPoints = maxHitPoints;
            rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            leftPoint.parent = null;
            rightPoint.parent = null;
            _movingRight = true;
            _moveCount = moveTime;
        }
    
        private void Update()
        {
            _anim.SetBool($"isMoving", !(rb.velocity.x <= 0));

            if (_moveCount > 0)
            {
                _moveCount -= Time.deltaTime;
                if (_movingRight)
                {
                    sp.flipX = true;
                    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                    if (transform.position.x > rightPoint.position.x)
                    {
                        _movingRight = false;
                    }
                }
                else
                {
                    sp.flipX = false;
                    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                    if (transform.position.x < leftPoint.position.x)
                    {
                        _movingRight = true;
                    }
                }

                if (_moveCount <= 0)
                {
                    _waitCount = Random.Range(waitTime * 0.75f, waitTime * 1.25f);
                }

                _anim.SetBool($"isMoving", true);
            }
            else if (_waitCount > 0)
            {
                _waitCount -= Time.deltaTime;
                rb.velocity = new Vector2(0f, rb.velocity.y);
                if (_waitCount <= 0)
                {
                    _moveCount = Random.Range(moveTime * 0.75f, waitTime * 0.75f);
                }

                _anim.SetBool($"isMoving", false);
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
            LevelManager.GemsCollected++;
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
            for(var i = 0; i <4; i++)
            {
                frogs[i].transform.position = data.frogPos[i];
            }
        }

        public void SaveData(ref GameData data)
        {
            for(var i = 0; i <4; i++)
            {
                data.frogPos[i] = frogs[i].transform.position;
            }
        }
    }
}