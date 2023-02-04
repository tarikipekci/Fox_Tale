using UnityEngine;

public class GetFireBall : MonoBehaviour
{
    [Header("Game Objects")] public GameObject fireFlash;
    public GameObject enemy;
    public GameObject block;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlaySfx(6);
            gameObject.SetActive(false);
            fireFlash.SetActive(true);
            if (TutorialController.instance.speechFive == 0 && TutorialController.instance.speechFour != 0)
            {
                TutorialController.instance.spOfSb.sprite = TutorialController.instance.speeches[6];
                TutorialController.instance.speechFive++;
                enemy.SetActive(true);
                Instantiate(EnemyController1.instance.deathEffect, enemy.transform.position, enemy.transform.rotation);
            }
        }

        if (other.gameObject.CompareTag("Grid"))
        {
            Destroy(block);
        }
    }
}