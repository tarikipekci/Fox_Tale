using System;
using UnityEngine;

namespace Enemy
{
    public class DartFishController : MonoBehaviour
    {
        [Header("Components")] public Transform player;
        private Rigidbody2D _rigidbody;
        private Vector3 _target;

        [Header("Objects")] public GameObject gem, warningSign;

        [Header("Movement Variables")] public float swimmingSpeed;
        private bool _isWarned;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var distance = Vector2.Distance(player.position, transform.position);

            if (Time.timeScale == 1)
            {
                if (distance < 50f)
                {
                    if (_isWarned == false)
                    {
                        _target = new Vector3(player.position.x, player.position.y, player.position.z);
                        var direction = -_target + transform.position;
                        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                        _rigidbody.rotation = angle;
                        Instantiate(warningSign, player.transform.position, player.transform.rotation);
                        _isWarned = true;
                    }

                    transform.position = Vector2.MoveTowards(transform.position, _target, swimmingSpeed);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                Instantiate(gem, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
    }
}