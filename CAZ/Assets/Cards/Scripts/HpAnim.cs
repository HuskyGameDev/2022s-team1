using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpAnim : MonoBehaviour
{
    public GameObject bigHeart;
    public bool active = true;
    public float delay;

    public float timer1 = 0.5f;
    public float timer2 = 1f;
    float time = 0f;

    private void Start()
    {
        time = -delay;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timer2 && active)
        {
            bigHeart.SetActive(false);
            active = false;
            time = 0f;
        }
        else if (time > timer1 && !active) {
            bigHeart.SetActive(true);
            active = true;
            time = 0f;
        }
    }
}
