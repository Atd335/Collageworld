using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldNamePasser : MonoBehaviour
{
    public static string worldName;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
