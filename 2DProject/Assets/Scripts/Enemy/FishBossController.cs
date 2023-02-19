using Level;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class FishBossController : MonoBehaviour
    {
        [Header("Movement Variables")] public float moveSpeed;
        private bool _movingUp;
        public float moveTime, waitTime;
        private float _moveCount, _waitCount;
        public int hitPoints;
        public int maxHitPoints;
        public float distance;
        public float waitToSpawn;
        private float _waitToSpawnCounter;
        
        [Header("Components")] public Transform upPoint, downPoint;
        public Rigidbody2D rb;
        public Transform player;
        public Text currentHealthValue;
        public Slider sliderLevel;
        public Transform dartFishSpawnerPoint, redFishSpawnerPoint1, redFishSpawnerPoint2;
        
        [Header("Objects")] public GameObject dartFish;
        public GameObject redFish;
        private void Start()
        {
            _waitToSpawnCounter = waitToSpawn;
            hitPoints = maxHitPoints;
            rb = GetComponent<Rigidbody2D>();
            upPoint.parent = null;
            downPoint.parent = null;
            _movingUp = true;
            _moveCount = moveTime;
        }

        private void Update()
        {
            distance = player.position.x - transform.position.x;
            var chanceToSpawn = Random.Range(0f, 100f);
            currentHealthValue.text = hitPoints + "/" + maxHitPoints;

            if (waitToSpawn > 0f)
            {
                waitToSpawn -= Time.deltaTime;
            }

            if (waitToSpawn <= 0f)
            {
                if (chanceToSpawn > 70f)
                {
                    var newFish = Instantiate(dartFish, dartFishSpawnerPoint.position, dartFishSpawnerPoint.rotation);
                    Destroy(newFish, 0.7f);
                    Instantiate(UnderWaterTraps.instance.deathEffect, newFish.gameObject.transform.position,
                        newFish.gameObject.transform.rotation);
                }
                else
                    switch (chanceToSpawn)
                    {
                        case < 40f:
                        {
                            var newFish = Instantiate(redFish, redFishSpawnerPoint1.position,
                                redFishSpawnerPoint1.rotation);
                            Destroy(newFish, 2.3f);
                            Instantiate(UnderWaterTraps.instance.deathEffect, newFish.gameObject.transform.position,
                                newFish.gameObject.transform.rotation);
                            break;
                        }
                        case < 60f:
                        {
                            var newFish = Instantiate(redFish, redFishSpawnerPoint2.position,
                                redFishSpawnerPoint2.rotation);
                            Destroy(newFish, 2.3f);
                            Instantiate(UnderWaterTraps.instance.deathEffect, newFish.gameObject.transform.position,
                                newFish.gameObject.transform.rotation);
                            break;
                        }
                        default:
                            return;
                    }

                waitToSpawn = _waitToSpawnCounter;
            }

            if (Mathf.Abs(distance) > 40)
            {
                rb.velocity = new Vector2(rb.velocity.x - 0.2f, rb.velocity.y);
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;

                if (_moveCount > 0)
                {
                    _moveCount -= Time.deltaTime;
                    if (_movingUp)
                    {
                        rb.velocity = new Vector2(0f, moveSpeed);
                        if (transform.position.y > upPoint.position.y)
                        {
                            _movingUp = false;
                        }
                    }
                    else
                    {
                        rb.velocity = new Vector2(0f, -moveSpeed);
                        if (transform.position.y < downPoint.position.y)
                        {
                            _movingUp = true;
                        }
                    }

                    if (_moveCount <= 0)
                    {
                        _waitCount = Random.Range(waitTime * 0.85f, waitTime * 1.25f);
                    }
                }
                else if (_waitCount > 0)
                {
                    _waitCount -= Time.deltaTime;
                    rb.velocity = new Vector2(0f, 0f);
                    if (_waitCount <= 0)
                    {
                        _moveCount = Random.Range(moveTime * 0.75f, waitTime * 0.75f);
                    }
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Bullet")) return;
            hitPoints--;
            sliderLevel.value--;
        }
    }
}