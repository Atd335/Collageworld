using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayWorldsScripts : MonoBehaviour
{

    public GameObject entry;

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

        for (int i = 0; i < names.Length; i++)
        {
            GameObject e = Instantiate(entry, transform);
            e.transform.localPosition = new Vector3(-5, 9 - (5*i), 1);
            e.GetComponent<LoadEntryInfo>().worldName.text = names[i].Substring(26);
            e.GetComponent<LoadEntryInfo>().worldDate.text = Directory.GetCreationTime(names[i]).ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {


        if (transform.childCount > 4)
        {
            transform.position += new Vector3(0,Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 3000,0);
        }

        if (transform.position.y > 5 * (transform.childCount - 4))
        {
            transform.position = new Vector3(-40, 5 * (transform.childCount - 4), 0);
        }

        if (transform.position.y < 0)
        {
            transform.position = new Vector3(-40,0, 0);
        }
    }
}
