using System;
using System.Collections;
using UnityEngine;

public class MovingTrapController : MonoBehaviour
{
   [Header("Components")] private Rigidbody2D _rigidbody;
   public Transform[] waypoints;

   [Header("Movement Variables")] public float speed;
   private int index;

   private void Awake()
   {
      waypoints[0].SetParent(null);
      waypoints[1].SetParent(null);
      _rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
      transform.position = Vector2.MoveTowards(transform.position, waypoints[index].position, speed * Time.deltaTime);
      MoveToTheWaypoint();
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         PlayerHealthController.instance.DealDamage(PlayerController.instance.damage);
      }

      if (other.gameObject.CompareTag("Bullet"))
      {
         Destroy(other.gameObject);
      }
   }
   
   private void MoveToTheWaypoint()
   {
      if (transform.position == waypoints[index].position)
      {
         if (index == waypoints.Length - 1)
         {
            index = 0;
         }
         else
         {
            index++;
         }
      }
   
   }
}
