using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [Header("Game Objects")] public GameObject block;
    public GameObject firingPos;

    [Header("Components")]
    public SpriteRenderer spOfArrow;

    [Header("Variables")] public float firingDuration;
    private float firingDurationCounter;
    public float fireSpeed;
    public float waitToFire;
    private float waitToFireCounter;
    private int amount;

    private void Start()
    {
        waitToFireCounter = waitToFire;
        firingDurationCounter = firingDuration;
        amount = 0;
    }

    private void Update()
    {
        if (waitToFire > 0)
        {
            firingDuration = firingDurationCounter;
            waitToFire -= Time.deltaTime;
            amount = 0;
        }
        else
        {
            if (amount == 0)
            {
                var newArrow = Instantiate(block, firingPos.transform.position, transform.rotation);

                if (gameObject.GetComponent<SpriteRenderer>().flipX)
                {
                    newArrow.GetComponent<Rigidbody2D>().velocity = new Vector2(fireSpeed * Time.fixedDeltaTime, 0f);
                    newArrow.GetComponent<SpriteRenderer>().flipX = true;
                    amount++;
                }
                else if (!gameObject.GetComponent<SpriteRenderer>().flipX)
                {
                    newArrow.GetComponent<Rigidbody2D>().velocity = -new Vector2(fireSpeed * Time.fixedDeltaTime, 0f);
                    amount++;
                }
            }

            firingDuration -= Time.deltaTime;

            if (firingDuration <= 0)
            {
                waitToFire = waitToFireCounter;
            }
        }
    }
}