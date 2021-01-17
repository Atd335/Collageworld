using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingJoke : MonoBehaviour
{

    AudioSource AS;

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
        doneLoading = false;
        loadTime = Random.Range(4,7f);
    }

    float loadTime = 4;
    float loadTimer = 0;

    public static bool doneLoading;

    // Update is called once per frame
    void Update()
    {
        loadTimer += Time.deltaTime;
        if (loadTimer>=loadTime)
        {
            doneLoading = true;
        }
        if (!doneLoading)
        {
            AS.volume = Mathf.Lerp(AS.volume, .15f, Time.deltaTime * 2);
        }
        else
        {
            AS.volume = Mathf.Lerp(AS.volume, -.50f, Time.deltaTime * 2);
        }
    }
}
