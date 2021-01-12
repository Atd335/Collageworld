using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShadowText : MonoBehaviour
{
    public TextMeshPro t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshPro>().text = t.text;
    }
}
