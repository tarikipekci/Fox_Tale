using System;
using System.Collections;
using UnityEngine;

public class LevelManagerForOceanside : MonoBehaviour
{
    [Header("Components")] private Rigidbody2D rb;
    private Rigidbody2D rbCam;

    [Header("Objects")] public GameObject player;
    public GameObject bgm;
    public GameObject informationPage;
    public GameObject screen;
    public GameObject farBG, foreBG, sandBG;

    [Header("Scripts")] public static LevelManagerForOceanside instance;

    [Header("Variables")] private float counterForInfoPage = 10f;

    public void Awake()
    {
        instance = this;
        bgm.SetActive(false);
        informationPage.SetActive(true);

        rb = player.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        rbCam = screen.GetComponent<Rigidbody2D>();
        rbCam.gravityScale = 0f;
        
        MuzzleFlash.instance.gameObject.SetActive(false);
       
    }

    private void Start()
    {
        PlayerController.instance.isUnderWater = true;
    }

    private void Update()
    {
        if (counterForInfoPage > 0f)
        {
            counterForInfoPage -= Time.deltaTime;
        }

        if (counterForInfoPage <= 0f)
        {
            bgm.SetActive(true);
            MuzzleFlash.instance.gameObject.SetActive(true);
            informationPage.SetActive(false);
        }

        if (counterForInfoPage <= 0)
        {
            rb.velocity = new Vector2(7f, 0f);

            if (Time.timeScale == 1)
            {
                farBG.transform.position = new Vector3(farBG.transform.position.x - 0.035f, farBG.transform.position.y,
                    farBG.transform.position.z);

                foreBG.transform.position = new Vector3(foreBG.transform.position.x - 0.035f,
                    foreBG.transform.position.y,
                    foreBG.transform.position.z);

                sandBG.transform.position = new Vector3(sandBG.transform.position.x - 0.05f,
                    sandBG.transform.position.y,
                    sandBG.transform.position.z);
            }

            rbCam.velocity = new Vector2(7f, 0f);
            PlayerController.instance.spriteRenderer.flipX = false;

            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 15f);
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 15f);
            }
        }
    }
}