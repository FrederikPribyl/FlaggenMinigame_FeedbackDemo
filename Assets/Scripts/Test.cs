using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    double timer;

    ParticleSystem ps;

    private void Start()
    {
        timer = Time.timeAsDouble;

        IEnumerator counter = Counter(1000);

        StartCoroutine(counter);

    }

    IEnumerator Counter(int max)
    {
        for (;;)
        {
            Debug.Log(Time.deltaTime);
            yield return null;
        }

        // Debug.Log(Time.timeAsDouble - timer);
    }

    // IEnumerator Counter()
    // {
    //     for (; ; )
    //     {
    //         Debug.Log("test");
    //     }

    //     // Debug.Log(Time.timeAsDouble - timer);
    // }

    IEnumerator Fade(Renderer renderer)
    {
        Color c = renderer.material.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            renderer.material.color = c;
            yield return null;
        }
    }

    
}
