using UnityEngine;

public class FlyingPlatformController : MonoBehaviour
{
    public Transform[] wayPoints;
    public float moveSpeed;
    public int target;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[target].position, moveSpeed * Time.deltaTime);
    }
    
    private void FixedUpdate()
    {
        if (transform.position == wayPoints[target].position)
        {
            if (target == wayPoints.Length - 1)
            {
                target = 0;
            }
            else
            {
                target++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            col.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.collider.transform.SetParent(null);
        }
    }
}