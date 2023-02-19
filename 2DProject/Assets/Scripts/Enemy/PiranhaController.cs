using Level;
using UnityEngine;

namespace Enemy
{
    public class PiranhaController : MonoBehaviour
    {
        [Header("Components")] public Transform player;
        private Rigidbody2D _rigidbody;

        [Header("Objects")] public GameObject gem;

        [Header("Movement Variables")] public float swimmingSpeed;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var direction = -player.position + transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rigidbody.rotation = angle;
            var distance = Vector2.Distance(player.position, transform.position);

            if (Time.timeScale == 1)
            {
                if (distance < 50f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, swimmingSpeed);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (OceanPartBossController.instance.enteredBossArea) return;
            if (other.gameObject.CompareTag("Bullet"))
            {
                Instantiate(gem, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
    }
}