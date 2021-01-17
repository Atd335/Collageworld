using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveStage : MonoBehaviour
{
    GameObject Images;
    public string worldName;

    private void Awake()
    {
        worldName = WorldNamePasser.worldName;
    }

    // Start is called before the first frame update
    void Start()
    {   
        Images = GameObject.Find("ALL_IMAGES");
        SaveWorld();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && worldName!="")
        {
            //SaveWorld();
        }
    }

    public void SaveWorld()
    {
        if (worldName == "" || worldName==null) { return; }
        if (Directory.Exists("C:/CollageWorld/SavedInfo/" + $"{worldName}"))
        if (Directory.Exists("C:/CollageWorld/SavedInfo/" + $"{worldName}"))
        { Directory.Delete("C:/CollageWorld/SavedInfo/" + $"{worldName}", true); }

        Directory.CreateDirectory("C:/CollageWorld/SavedInfo/" + $"{worldName}");

        if (File.Exists("C:/CollageWorld/SavedInfo/" + $"{worldName}/{worldName}.txt"))
        { File.Delete("C:/CollageWorld/SavedInfo/" + $"{worldName}/{worldName}.txt"); }

        StreamWriter writer = File.AppendText("C:/CollageWorld/SavedInfo/" + $"{worldName}/{worldName}.txt");

        writer.WriteLine($"{Images.transform.childCount}");
        writer.WriteLine($"------------------------------------------");

        foreach (TextureHavenScript i in Images.GetComponentsInChildren<TextureHavenScript>())
        {
            byte[] savedImage = i.editTex.EncodeToPNG();
            File.WriteAllBytes("C:/CollageWorld/SavedInfo/" + $"{worldName}/" + i.IMAGEID.ToString() + ".png", savedImage);
            writer.WriteLine($"{i.IMAGEID}");
            writer.WriteLine($"POS: \n{i.transform.position.x},{i.transform.position.y},{i.transform.position.z}");
            writer.WriteLine($"ROT: \n{i.transform.rotation.eulerAngles.x},{i.transform.rotation.eulerAngles.y},{i.transform.rotation.eulerAngles.z}");
            writer.WriteLine($"SIZ: \n{i.transform.localScale.x},{i.transform.localScale.y},{i.transform.localScale.z}");
            writer.WriteLine($"COL: \n{i.currentColor.r},{i.currentColor.g},{i.currentColor.b},{i.currentColor.a}");
            writer.WriteLine($"SKT: \n{i.sketchified.ToString()[0]}");
            writer.WriteLine($"------------------------------------------");
        }

        writer.Close();

        Debug.Log("SAVED!!!");
    }
}
