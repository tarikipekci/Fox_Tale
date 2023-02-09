using System.Collections;
using Player;
using UnityEngine;

namespace Level
{
    public class CoconutBuff : MonoBehaviour
    {
        public float buffDuration;
        private bool _isCollected;
        public SpriteRenderer sp;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player") && !_isCollected && buffDuration > 0)
            {
                _isCollected = true;
                PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
                UIController.instance.UpdateHealth();
                PlayerController.instance.moveSpeed += 7;
                StartCoroutine(BuffEffect());
                sp.enabled = false;
            }
        }

        private void Update()
        {
            if(_isCollected)
            {
                buffDuration -= Time.deltaTime;
                if (buffDuration <= 0)
                {
                    PlayerController.instance.moveSpeed -= 7;
                    Destroy(gameObject);
                }
            }
        
        }

        IEnumerator BuffEffect()
        {
            yield return new WaitForSeconds(0.2f);
            PlayerController.instance.spriteRenderer.color = new Color(0.5f, PlayerController.instance.spriteRenderer.color.g, PlayerController.instance.spriteRenderer.color.b,
                PlayerController.instance.spriteRenderer.color.a);
            yield return new WaitForSeconds(0.2f);
            PlayerController.instance.spriteRenderer.color = new Color(1f, PlayerController.instance.spriteRenderer.color.g, PlayerController.instance.spriteRenderer.color.b,
                PlayerController.instance.spriteRenderer.color.a);
        }
    }
}