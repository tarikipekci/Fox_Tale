using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [Header("Variables")]
    public float lifeTime;
    
    void Update()                                                                                                       //after a certain time destroying the effect
    {
        Destroy(gameObject,lifeTime);
    }
}
