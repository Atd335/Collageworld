using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconOrient : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int id;


    // Update is called once per frame
    void Update()
    {
        if (InteractorScript.interactMode == id)
        {
            transform.localScale = Vector3.Lerp(transform.localScale,Vector3.one*.3f,Time.deltaTime * 10);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * .15f, Time.deltaTime * 10);
        }

        transform.up = Vector3.Lerp(transform.up, Vector3.up, Time.deltaTime * 10);
    }
}
