using Player;
using UnityEngine;

public class GoDownController : MonoBehaviour
{
    
    [Header("Components")]
    public Animator anim;
    private PlatformEffector2D effector2D;
    
    private void Start()
    {
        effector2D = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && PlayerController.instance.isPlatform)
        {
            effector2D.rotationalOffset = 180f;
            anim.SetBool($"goingDown",true);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            effector2D.rotationalOffset = 0f;
        }
    }
}