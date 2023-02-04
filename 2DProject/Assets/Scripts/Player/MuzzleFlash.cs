using System;
using System.Collections;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [Header("Objects")] public GameObject bullet;
    
    [Header("Components")] private Rigidbody2D rb;

    [Header("Variables")] public bool isShooting;
    [SerializeField] public float bulletVelocity;
    [SerializeField] private float shootTimer;

    [Header("Scripts")] public static MuzzleFlash instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        isShooting = false;
    }

    private void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {
            if (PlayerController.instance.knockBackCounter <= 0)
            {
                Fire();
            }
        }
    }
    
    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isShooting)
        {
            StartCoroutine(Shoot());
        }
    }
    
    private IEnumerator Shoot()
    {
        isShooting = true;
        AudioManager.instance.PlaySfx(2);
        var newBullet =
            Instantiate(bullet, gameObject.transform.position,
                transform.rotation); // Creating clons of the original bullet object to destroy.

        newBullet.GetComponent<Rigidbody2D>().velocity = PlayerController.instance.spriteRenderer.flipX switch
        {
            false => new Vector2(bulletVelocity * Time.fixedDeltaTime, 0f),
            true => -new Vector2(bulletVelocity * Time.fixedDeltaTime, 0f)
        };

        yield return new WaitForSeconds(shootTimer);
        isShooting = false;
        yield return new WaitForSeconds(1f);
        Destroy(newBullet); // Destroying the clon of the bullet object
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            bullet.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}