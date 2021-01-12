using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyTex : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one * (1+((Mathf.Sin(Time.time)/2)+.5f));
    }
}
