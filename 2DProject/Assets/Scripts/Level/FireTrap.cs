using UnityEngine;

namespace Level
{
    public class FireTrap : MonoBehaviour
    {
        [Header("Game Objects")] public GameObject block;
        public GameObject firingPos;

        [Header("Components")]
        public SpriteRenderer spOfArrow;

        [Header("Variables")] public float firingDuration;
        private float _firingDurationCounter;
        public float fireSpeed;
        public float waitToFire;
        private float _waitToFireCounter;
        private int _amount;

        private void Start()
        {
            _waitToFireCounter = waitToFire;
            _firingDurationCounter = firingDuration;
            _amount = 0;
        }

        private void Update()
        {
            if (waitToFire > 0)
            {
                firingDuration = _firingDurationCounter;
                waitToFire -= Time.deltaTime;
                _amount = 0;
            }
            else
            {
                if (_amount == 0)
                {
                    var newArrow = Instantiate(block, firingPos.transform.position, transform.rotation);

                    if (gameObject.GetComponent<SpriteRenderer>().flipX)
                    {
                        newArrow.GetComponent<Rigidbody2D>().velocity = new Vector2(fireSpeed * Time.fixedDeltaTime, 0f);
                        newArrow.GetComponent<SpriteRenderer>().flipX = true;
                        _amount++;
                    }
                    else if (!gameObject.GetComponent<SpriteRenderer>().flipX)
                    {
                        newArrow.GetComponent<Rigidbody2D>().velocity = -new Vector2(fireSpeed * Time.fixedDeltaTime, 0f);
                        _amount++;
                    }
                }

                firingDuration -= Time.deltaTime;

                if (firingDuration <= 0)
                {
                    waitToFire = _waitToFireCounter;
                }
            }
        }
    }
}