using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class OceanBGController : MonoBehaviour
{
    [Header("Components")] private Rigidbody2D _rigidbody;
    public Transform[] waypoints;
    public Transform player;

    [Header("Variables")] private int index;
    public float speed;

    private void Awake()
    {
        waypoints[0].SetParent(null);
        waypoints[1].SetParent(null);
        _rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var distance = Vector2.Distance(gameObject.transform.position, player.position);

        if (distance < 50f)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, waypoints[index].position, speed * Time.deltaTime);
            MoveToTheWaypoint();
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