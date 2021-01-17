using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSHADE : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.x > 39)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, new Color(1,1,1,.8f),Time.deltaTime * 10);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
    }
}
