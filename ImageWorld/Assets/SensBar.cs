using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensBar : MonoBehaviour
{
    public PlayerMovement PM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3((PM.mouseSens * 2)/100,1,1), Time.deltaTime * 10);
    }
}
