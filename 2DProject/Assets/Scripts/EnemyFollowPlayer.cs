using System;
using System.Collections;
using System.Collections.Generic;
/*using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    [Header("Components")] public Animator anim;
    public Rigidbody2D rb;

    [Header("Variables")] public float speed;
    public float lineOfSite;
    public float distance;

    [Header("Vectors")] private Vector2 spawnPoint;

    private void Awake()
    {
        spawnPoint = transform.position;
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(PlayerController.instance.transform.position, transform.position);
        distance = distanceFromPlayer;
        if (distanceFromPlayer < lineOfSite)
        {
            anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
            transform.position = Vector2.MoveTowards(this.transform.position,
                PlayerController.instance.transform.position,
                speed * Time.deltaTime);
            spawnPoint = new Vector2(transform.position.x, transform.position.y + 5);
        }
        else
        {
            anim.SetFloat("moveSpeed", 0f);
            
           /* if (EnemyAIController.instance.sp.flipX)
            {
                EnemyAIController.instance.sp.flipX = false;
            }
            else if (EnemyAIController.instance.sp.flipX == false)
            {
                EnemyAIController.instance.sp.flipX = true;
            }

            transform.position = Vector2.MoveTowards(this.transform.position, spawnPoint, speed * Time.deltaTime);
        }
    }

  /*  private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
   } }*/