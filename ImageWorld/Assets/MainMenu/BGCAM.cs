using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCAM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public static float spd;
    // Update is called once per frame
    void Update()
    {
        spd = Mathf.Lerp(spd,1,Time.deltaTime * 10);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(22,transform.rotation.eulerAngles.y,0),Time.deltaTime * 2);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Mathf.Sin(Time.time*spd)*2, Mathf.Sin(Time.time) * 2);
    }
}
