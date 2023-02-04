using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    private int counter;
    public GameObject[] water;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        StartCoroutine(CounterForAnimation());
    }

    IEnumerator CounterForAnimation()
    {
        water[counter].GetComponent<Animator>().SetBool($"Turn", true);
        yield return new WaitForSeconds(2f);
        water[counter].GetComponent<Animator>().SetBool($"Turn", false);
        counter++;

        if (counter == water.Length)
        {
            counter = 0;
        }
        yield return new WaitForSeconds(2f);
    }
}