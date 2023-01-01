using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Scripts")] public static UIController instance;

    [Header("Images")] [SerializeField] public Image[] hearts;
    public Image fadeScreen;
    public Image deathScreen;

    [Header("Components")] public Sprite heartFull, heartHalf, heartEmpty;
    public Text gemText;

    [Header("Variables")] public float fadeSpeed;
    private bool shouldFadeToWhite, shouldFadeFromWhite;

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
        if (shouldFadeToWhite)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToWhite = false;
            }
        }

        if (shouldFadeFromWhite)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromWhite = false;
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
        gemText.text = LevelManager.gemsCollected.ToString();
    }

    public void FadeToBlack()
    {
        shouldFadeToWhite = true;
        shouldFadeFromWhite = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeFromWhite = true;
        shouldFadeToWhite = false;
    }
}