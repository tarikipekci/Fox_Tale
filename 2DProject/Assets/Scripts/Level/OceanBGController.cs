using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class OceanBGController : MonoBehaviour
{
    [Header("Objects")] public GameObject[] fishes;

    [Header("Components")] public Rigidbody2D[] _rigidbody;
    
    [Header("Variables")] private int index;

    private void Awake()
    {
        while (index < fishes.Length)
        {
            _rigidbody[index] = fishes[index].GetComponent<Rigidbody2D>();
            index++;
        }

        index = 0;
    }

    private void Update()
    {
        while (index < fishes.Length)
        {
            _rigidbody[index].velocity = new Vector2(-1f, 1f);
            index++;
        }
    }
}