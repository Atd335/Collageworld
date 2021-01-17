using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DisplayWorldsScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!Directory.Exists(@"C:/CollageWorld/SavedInfo"))
        {
            Directory.CreateDirectory(@"C:/CollageWorld/SavedInfo");
        }
        string[] names = Directory.GetDirectories(@"C:/CollageWorld/SavedInfo");
        foreach (string str in names)
        {
            print(str);
            print(Directory.GetCreationTime(str));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
