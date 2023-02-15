using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Level
{
    public class OceanSidePart2Controller : MonoBehaviour
    {
        [FormerlySerializedAs("CameraTransform")] [Header("Components")]
        public Transform cameraTransform;

        [Header("Components")] public Transform player;

        [Header("Variables")] public bool part2;
        [Header("Objects")] public GameObject playerLight;

        [Header("Scripts")]
        // ReSharper disable once InconsistentNaming
        public static OceanSidePart2Controller instance;

        public Light2D globalLight;


        private void Awake()
        {
            instance = this;
            part2 = false;
        }

        private IEnumerator WhiteToBlackBlackToWhite()
        {
            UIController.instance.FadeToBlack();
            yield return new WaitForSeconds(2.5f);
            cameraTransform.position = new Vector3(cameraTransform.position.x + 950, cameraTransform.position.y, -10f);
            player.position = new Vector3(player.position.x + 950, player.position.y, 0f);
            part2 = true;
            playerLight.SetActive(true);
            globalLight.color = new Color(0.28f, 0.19f, 0.19f, 1f);
            UIController.instance.FadeFromBlack();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(WhiteToBlackBlackToWhite());
            }
        }
    }
}