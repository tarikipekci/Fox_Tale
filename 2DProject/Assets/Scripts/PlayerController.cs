using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    [Header("Scripts")] public static PlayerController instance;

    [Header("Components")] private TrailRenderer trailRenderer;
    public Rigidbody2D rb;
    public Transform groundCheckPoint;

    [FormerlySerializedAs("_spriteRenderer")]
    public SpriteRenderer spriteRenderer;

    public Animator anim;

    [Header("Movements")] public float moveSpeed;
    public float jumpForce;
    public bool canDoubleJump;
    private int jumpingCounter;
    private int doubleJumpCounter;
    public bool isUnderWater;

    [Header("Ground Check")] public LayerMask whatIsGround;
    public LayerMask spike;
    public LayerMask platform;
    public LayerMask bouncer;
    private bool isSpike;
    public bool isGrounded;
    public bool isPlatform;
    public bool isBouncer;

    [Header("Objects")] public GameObject bullet;
    public GameObject muzzleFlash;
    public GameObject shootingPos2;
    public GameObject shootingPos1;

    [Header("Knockback Variables")] public float knockBackLength, knockBackForce;
    public float knockBackCounter;
    public int damage = 1;
    public float bounceForce;

    [Header("Dashing Variables")] [SerializeField]
    public float dashingVelocity = 14f;

    private int dashingCounter;
    public int counter;

    [SerializeField] public float dashingTime = 0.5f;
    private Vector2 dashingDir;
    public bool isDashing;
    public bool canDash = true;
    private float gravityScale;
    public bool isGoingDown;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        gravityScale = rb.gravityScale;
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
        this.spriteRenderer.flipX = data.direction;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
        data.direction = this.spriteRenderer.flipX;
    }

    private void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {
            if (isUnderWater == false)
            {
                if (knockBackCounter <= 0)
                {
                    rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);

                    var position = groundCheckPoint.position;
                    isBouncer = Physics2D.OverlapCircle(position, .2f, bouncer);

                    isGrounded = Physics2D.OverlapCircle(position, .2f, whatIsGround);

                    isSpike = Physics2D.OverlapCircle(position, 0.2f, spike);

                    isPlatform = Physics2D.OverlapCircle(position, 0.2f, platform);

                    var inputX = Input.GetAxisRaw("Horizontal");

                    if (dashingCounter == 0)
                    {
                        canDash = true;
                    }

                    if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
                    {
                        isDashing = true;
                        canDash = false;
                        canDoubleJump = false;
                        trailRenderer.emitting = true;

                        var originalGravityScale = rb.gravityScale;
                        rb.gravityScale = 0f;

                        //Detecting direction of the player
                        if (spriteRenderer.flipX)
                        {
                            dashingDir = -new Vector2(inputX, Input.GetAxisRaw("Vertical"));
                        }

                        else if (!spriteRenderer.flipX)
                        {
                            dashingDir = new Vector2(inputX, Input.GetAxisRaw("Vertical"));
                        }

                        if (dashingDir == Vector2.zero && spriteRenderer.flipX)
                        {
                            dashingDir = new Vector2(transform.localScale.x, 0);
                        }
                        else if (dashingDir == Vector2.zero && !spriteRenderer.flipX)
                        {
                            dashingDir = new Vector2(transform.localScale.x, 0);
                        }

                        StartCoroutine(StopDashing(originalGravityScale));
                    }

                    if (rb.velocity.y < 0)
                    {
                        isGoingDown = true;
                    }
                    else
                    {
                        isGoingDown = false;
                        anim.SetBool($"isGrounded", false);
                    }

                    anim.SetBool($"goingDown", isGoingDown);

                    if (isDashing && !isGrounded && !isPlatform)
                    {
                        if (counter != 1)
                        {
                            canDoubleJump = true;
                        }

                        anim.SetBool($"isDashing", isDashing);
                        AudioManager.instance.PlaySfx(12);
                        switch (spriteRenderer.flipX)
                        {
                            case true:
                                rb.velocity = -dashingDir.normalized * dashingVelocity;
                                dashingCounter++;
                                break;
                            case false:
                                rb.velocity = dashingDir.normalized * dashingVelocity;
                                dashingCounter++;
                                break;
                        }

                        rb.gravityScale = gravityScale;
                        return;
                    }

                    if (isSpike && PlayerHealthController.instance.invincibleCounter <= 0)
                    {
                        PlayerHealthController.instance.DealDamage(damage);
                    }

                    if (isGrounded || isPlatform)
                    {
                        counter = 0;
                        canDash = false;
                        canDoubleJump = true;
                        dashingCounter = 0;
                        anim.SetBool($"goingDown", false);
                    }

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        if ((isGrounded || isPlatform) && jumpingCounter == 0)
                        {
                            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                            AudioManager.instance.PlaySfx(10);
                            jumpingCounter++;
                        }

                        else
                        {
                            if (canDoubleJump && doubleJumpCounter == 0)
                            {
                                counter++;
                                anim.SetBool($"isGrounded", isGrounded);
                                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                                AudioManager.instance.PlaySfx(10);
                                canDoubleJump = false;
                                doubleJumpCounter++;
                            }
                        }

                        jumpingCounter = 0;
                        doubleJumpCounter = 0;
                    }

                    if (rb.velocity.x < 0)
                    {
                        spriteRenderer.flipX = true;
                        shootingPos2.GetComponent<SpriteRenderer>().flipX = true;
                        bullet.GetComponent<SpriteRenderer>().flipX = true;
                        muzzleFlash.transform.position = shootingPos2.transform.position;
                    }
                    else if (rb.velocity.x > 0)
                    {
                        spriteRenderer.flipX = false;
                        shootingPos2.GetComponent<SpriteRenderer>().flipX = false;
                        bullet.GetComponent<SpriteRenderer>().flipX = false;
                        muzzleFlash.transform.position = shootingPos1.transform.position;
                    }
                }
                else
                {
                    knockBackCounter -= Time.deltaTime;

                    rb.velocity = spriteRenderer.flipX
                        ? new Vector2(knockBackForce, rb.velocity.y)
                        : new Vector2(-knockBackForce, rb.velocity.y);
                }
            }

            //Setting animations
            anim.SetFloat($"moveSpeed", Mathf.Abs(rb.velocity.x));
            anim.SetBool($"isGrounded", isGrounded);
            anim.SetBool($"isSpike", isSpike);
            anim.SetBool($"isPlatform", isPlatform);
            anim.SetBool($"isBouncer", isBouncer);
            anim.SetBool($"underWater", isUnderWater);
        }
    }


    private IEnumerator StopDashing(float gravityScaleWhileDashing)
    {
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        rb.gravityScale = gravityScaleWhileDashing;
        isDashing = false;
        anim.SetBool($"isDashing", isDashing);
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        if (spriteRenderer.flipX)
        {
            rb.velocity = new Vector2(-knockBackForce, knockBackForce);
        }
        else if (!spriteRenderer.flipX)
        {
            rb.velocity = new Vector2(knockBackForce, knockBackForce);
        }

        if (isUnderWater == false)
        {
            anim.SetTrigger($"hurt");
        }
    }

    public void Bounce()
    {
        rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        AudioManager.instance.PlaySfx(10);
    }
}