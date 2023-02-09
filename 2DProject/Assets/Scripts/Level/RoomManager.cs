using UnityEngine;

namespace Level
{
    public class RoomManager : MonoBehaviour
    {
        public GameObject virtualCam;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Player") && !other.isTrigger)
            {
                virtualCam.SetActive(true);
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Player") && !other.isTrigger)
            {
                virtualCam.SetActive(false);
            }
        }
    }
}
