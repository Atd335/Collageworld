using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldNamePasser : MonoBehaviour
{
    public static string worldName;
    public static bool isLoading;
    // Start is called before the first frame update
    void Start()
    {
        isLoading = false;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
