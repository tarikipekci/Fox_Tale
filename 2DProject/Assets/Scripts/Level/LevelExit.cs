using UnityEngine;

namespace Level
{
    public class LevelExit : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                LevelManager._instance.EndLevel();
            }
        }
    }
}
