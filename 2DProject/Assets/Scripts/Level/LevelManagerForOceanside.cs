using UnityEngine;

public class LevelManagerForOceanside : MonoBehaviour
{
    [Header("Components")] private Rigidbody2D rb;
    private Rigidbody2D rbBg;
    private Rigidbody2D rbCam;

    [Header("Objects")] public GameObject player;
    public GameObject screen;
    public GameObject bg;


    [Header("Scripts")] public static LevelManagerForOceanside instance;


    public void Awake()
    {
        instance = this;
        rb = player.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rbBg = bg.GetComponent<Rigidbody2D>();
        rbBg.gravityScale = 0f;
        rbCam = screen.GetComponent<Rigidbody2D>();
        rbCam.gravityScale = 0f;
    }

    private void Update()
    {
        PlayerController.instance.isUnderWater = true;
        rb.velocity = new Vector2(7f, 0f);
        rbBg.velocity = new Vector2(7f, 0f);
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