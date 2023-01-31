using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnimation : MonoBehaviour
{

    public int counter;
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
        water[counter].GetComponent<Animator>().SetBool($"Turn",true);
        yield return new WaitForSeconds(0.95f);
        counter++;
        water[counter - 1].GetComponent<Animator>().SetBool($"Turn",false);

        if (counter == water.Length)
        {
            counter = 0;
        }
        yield return new WaitForSeconds(0.95f);
    }
}
