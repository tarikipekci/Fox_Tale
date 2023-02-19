using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Level
{
    public class GlobalLightController : MonoBehaviour
    {
        [Header("Variables")] [SerializeField] public float lightingCounter;

        [Header("Scripts")] public Light2D globalLight, playerLight, fireballLight;

        [Header("Components")] public SpriteRenderer[] spOfBackground, spOfBgMid;

        [Header("Sprite")] public Sprite spriteBgFar, spriteBgMid;

        // ReSharper disable once InconsistentNaming
        public static GlobalLightController instance;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (!(BossController.instance.hitPoints <= 0)) return;
            if (!(lightingCounter > 0)) return;
            if (!(globalLight.intensity ! <= 1)) return;
            globalLight.intensity += 0.001f;
            globalLight.color = Color.white;
            lightingCounter -= Time.deltaTime;
            playerLight.gameObject.SetActive(false);
            fireballLight.gameObject.SetActive(false);
            spOfBackground[0].sprite = spriteBgFar;
            foreach (var t in spOfBgMid)
            {
                t.sprite = spriteBgMid;
            }
            spOfBackground[0].sprite = spriteBgFar;
        }
    }
}