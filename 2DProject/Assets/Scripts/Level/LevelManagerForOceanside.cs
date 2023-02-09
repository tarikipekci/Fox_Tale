using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    public class LevelManagerForOceanside : MonoBehaviour
    {
        [Header("Components")] private Rigidbody2D _rb;
        private Rigidbody2D _rbCam;

        [Header("Objects")] public GameObject player;
        public GameObject bgm, shootingSound;
        public GameObject informationPage;
        public GameObject screen;
        [FormerlySerializedAs("farBG")] public GameObject farBg;
        [FormerlySerializedAs("foreBG")] public GameObject foreBg;
        [FormerlySerializedAs("sandBG")] public GameObject sandBg;

        // ReSharper disable once InconsistentNaming
        [Header("Scripts")] public static LevelManagerForOceanside instance;

        [Header("Variables")] private float _counterForInfoPage = 10f;

        public void Awake()
        {
            instance = this;
            bgm.SetActive(false);
            informationPage.SetActive(true);

            _rb = player.GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0f;

            _rbCam = screen.GetComponent<Rigidbody2D>();
            _rbCam.gravityScale = 0f;

            shootingSound.SetActive(false);
        }

        private void Start()
        {
            PlayerController.instance.isUnderWater = true;
        }

        private void Update()
        {
            if (_counterForInfoPage > 0f)
            {
                _counterForInfoPage -= Time.deltaTime;
            }

            if (_counterForInfoPage <= 0f)
            {
                bgm.SetActive(true);
                shootingSound.SetActive(true);
                informationPage.SetActive(false);
            }

            if (_counterForInfoPage <= 0)
            {
                _rb.velocity = new Vector2(7f, 0f);

                if (Time.timeScale == 1)
                {
                    farBg.transform.position = new Vector3(farBg.transform.position.x - 0.035f, farBg.transform.position.y,
                        farBg.transform.position.z);

                    foreBg.transform.position = new Vector3(foreBg.transform.position.x - 0.035f,
                        foreBg.transform.position.y,
                        foreBg.transform.position.z);

                    sandBg.transform.position = new Vector3(sandBg.transform.position.x - 0.05f,
                        sandBg.transform.position.y,
                        sandBg.transform.position.z);
                }

                _rbCam.velocity = new Vector2(7f, 0f);
                PlayerController.instance.spriteRenderer.flipX = false;

                if (Input.GetKey(KeyCode.W))
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y + 15f);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y - 15f);
                }
            }
        }
    }
}