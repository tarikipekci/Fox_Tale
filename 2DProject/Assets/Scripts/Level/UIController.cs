using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class UIController : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        [Header("Scripts")] public static UIController instance;

        [Header("Images")] [SerializeField] public Image[] hearts;
        public Image fadeScreen;
        public Image deathScreen;

        [Header("Components")] public Sprite heartFull, heartHalf, heartEmpty;
        public Text gemText;

        [Header("Variables")] public float fadeSpeed;
        private bool _shouldFadeToWhite, _shouldFadeFromWhite;

        [Header("Game Objects")] public GameObject levelCompleteText;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            gemText.text = "0";
            FadeFromBlack();
        }

        private void Update()
        {
            if (_shouldFadeToWhite)
            {
                fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                    Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
                if (fadeScreen.color.a == 1f)
                {
                    _shouldFadeToWhite = false;
                }
            }

            if (_shouldFadeFromWhite)
            {
                fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                    Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
                if (fadeScreen.color.a == 0f)
                {
                    _shouldFadeFromWhite = false;
                }
            }
        }

        public void UpdateHealth()
        {
            var currentHealth = PlayerHealthController.instance.currentHealth;

            var fullHearthCount = currentHealth / 2;

            var isEvenHealth = currentHealth % 2 == 0;

            for (var i = 0; i < fullHearthCount && i < hearts.Length; i++)
            {
                hearts[i].sprite = heartFull;
            }

            for (var i = fullHearthCount; i < hearts.Length; i++)
            {
                hearts[i].sprite = heartEmpty;
            }

            if (isEvenHealth == false && fullHearthCount < hearts.Length)
            {
                hearts[fullHearthCount].sprite = heartHalf;
            }
        }

        public void UpdateGemCount()
        {
            gemText.text = LevelManager.GemsCollected.ToString();
        }

        public void FadeToBlack()
        {
            _shouldFadeToWhite = true;
            _shouldFadeFromWhite = false;
        }

        public void FadeFromBlack()
        {
            _shouldFadeFromWhite = true;
            _shouldFadeToWhite = false;
        }
    }
}