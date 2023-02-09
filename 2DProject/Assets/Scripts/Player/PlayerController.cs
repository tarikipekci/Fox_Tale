using System.Collections;
using Game;
using Level;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        [Header("Scripts")] public static PlayerController instance;

        [Header("Components")] private TrailRenderer _trailRenderer;
        public Rigidbody2D rb;
        public Transform groundCheckPoint;

        [FormerlySerializedAs("_spriteRenderer")]
        public SpriteRenderer spriteRenderer;

        public Animator anim;

        [Header("Movements")] public float moveSpeed;
        public float jumpForce;
        public bool canDoubleJump;
        private int _jumpingCounter;
        private int _doubleJumpCounter;
        public bool isUnderWater;

        [Header("Ground Check")] public LayerMask whatIsGround;
        public LayerMask spike;
        public LayerMask platform;
        public LayerMask bouncer;
        private bool _isSpike;
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

        private int _dashingCounter;
        public int counter;

        [SerializeField] public float dashingTime = 0.5f;
        private Vector2 _dashingDir;
        public bool isDashing;
        public bool canDash = true;
        private float _gravityScale;
        public bool isGoingDown;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            anim = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            _trailRenderer = GetComponent<TrailRenderer>();
            _gravityScale = rb.gravityScale;
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

                        _isSpike = Physics2D.OverlapCircle(position, 0.2f, spike);

                        isPlatform = Physics2D.OverlapCircle(position, 0.2f, platform);

                        var inputX = Input.GetAxisRaw("Horizontal");

                        if (_dashingCounter == 0)
                        {
                            canDash = true;
                        }

                        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
                        {
                            isDashing = true;
                            canDash = false;
                            canDoubleJump = false;
                            _trailRenderer.emitting = true;

                            var originalGravityScale = rb.gravityScale;
                            rb.gravityScale = 0f;

                            //Detecting direction of the player
                            if (spriteRenderer.flipX)
                            {
                                _dashingDir = -new Vector2(inputX, Input.GetAxisRaw("Vertical"));
                            }

                            else if (!spriteRenderer.flipX)
                            {
                                _dashingDir = new Vector2(inputX, Input.GetAxisRaw("Vertical"));
                            }

                            if (_dashingDir == Vector2.zero && spriteRenderer.flipX)
                            {
                                _dashingDir = new Vector2(transform.localScale.x, 0);
                            }
                            else if (_dashingDir == Vector2.zero && !spriteRenderer.flipX)
                            {
                                _dashingDir = new Vector2(transform.localScale.x, 0);
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
                                    rb.velocity = -_dashingDir.normalized * dashingVelocity;
                                    _dashingCounter++;
                                    break;
                                case false:
                                    rb.velocity = _dashingDir.normalized * dashingVelocity;
                                    _dashingCounter++;
                                    break;
                            }

                            rb.gravityScale = _gravityScale;
                            return;
                        }

                        if (_isSpike && PlayerHealthController.instance.invincibleCounter <= 0)
                        {
                            PlayerHealthController.instance.DealDamage(damage);
                        }

                        if (isGrounded || isPlatform)
                        {
                            counter = 0;
                            canDash = false;
                            canDoubleJump = true;
                            _dashingCounter = 0;
                            anim.SetBool($"goingDown", false);
                        }

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            if ((isGrounded || isPlatform) && _jumpingCounter == 0)
                            {
                                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                                AudioManager.instance.PlaySfx(10);
                                _jumpingCounter++;
                            }

                            else
                            {
                                if (canDoubleJump && _doubleJumpCounter == 0)
                                {
                                    counter++;
                                    anim.SetBool($"isGrounded", isGrounded);
                                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                                    AudioManager.instance.PlaySfx(10);
                                    canDoubleJump = false;
                                    _doubleJumpCounter++;
                                }
                            }

                            _jumpingCounter = 0;
                            _doubleJumpCounter = 0;
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
                anim.SetBool($"isSpike", _isSpike);
                anim.SetBool($"isPlatform", isPlatform);
                anim.SetBool($"isBouncer", isBouncer);
                anim.SetBool($"underWater", isUnderWater);
            }
        }


        private IEnumerator StopDashing(float gravityScaleWhileDashing)
        {
            yield return new WaitForSeconds(dashingTime);
            _trailRenderer.emitting = false;
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
}