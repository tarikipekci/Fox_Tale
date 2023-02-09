using System.Collections;
using UnityEngine;

namespace Level
{
    public class WaterAnimation : MonoBehaviour
    {
        private int _counter;
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
            water[_counter].GetComponent<Animator>().SetBool($"Turn", true);
            yield return new WaitForSeconds(2f);
            water[_counter].GetComponent<Animator>().SetBool($"Turn", false);
            _counter++;

            if (_counter == water.Length)
            {
                _counter = 0;
            }
            yield return new WaitForSeconds(2f);
        }
    }
}