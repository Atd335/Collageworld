using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SketchifyItem : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        initRot = transform.localRotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int frames;
    public int sketchSpd = 10;
    public float scaleDegree;
    public float rotDegree;

    float initRot;
    public float scale;

    private void FixedUpdate()
    {
        frames++;

        if (frames%sketchSpd==0)
        {
            transform.localScale = (Vector3.one*scale) + new Vector3(Random.Range(-scaleDegree,scaleDegree), Random.Range(-scaleDegree, scaleDegree), Random.Range(-scaleDegree, scaleDegree));
            transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, initRot + Random.Range(-rotDegree, rotDegree));
        }
    }
}
