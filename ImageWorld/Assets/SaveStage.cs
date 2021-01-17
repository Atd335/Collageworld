using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveStage : MonoBehaviour
{
    GameObject Images;
    public string worldName;
    // Start is called before the first frame update
    void Start()
    {
        
        Images = GameObject.Find("ALL_IMAGES");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && worldName!="")
        {
            SaveWorld();
        }
    }

    public void SaveWorld()
    {

        if (Directory.Exists("D:/CODE/CollageWorld/Collageworld/ImageWorld/Assets/SavedInfo/" + $"/{worldName}"))
        { Directory.Delete("D:/CODE/CollageWorld/Collageworld/ImageWorld/Assets/SavedInfo/" + $"/{worldName}", true); }

        Directory.CreateDirectory("D:/CODE/CollageWorld/Collageworld/ImageWorld/Assets/SavedInfo/" + $"/{worldName}");

        if (File.Exists("D:/CODE/CollageWorld/Collageworld/ImageWorld/Assets/SavedInfo/" + $"/{worldName}/{worldName}.txt"))
        { File.Delete("D:/CODE/CollageWorld/Collageworld/ImageWorld/Assets/SavedInfo/" + $"/{worldName}/{worldName}.txt"); }

        StreamWriter writer = File.AppendText("D:/CODE/CollageWorld/Collageworld/ImageWorld/Assets/SavedInfo/" + $"/{worldName}/{worldName}.txt");

        writer.WriteLine($"{Images.transform.childCount}");
        writer.WriteLine($"------------------------------------------");

        foreach (TextureHavenScript i in Images.GetComponentsInChildren<TextureHavenScript>())
        {
            byte[] savedImage = i.editTex.EncodeToPNG();
            File.WriteAllBytes("D:/CODE/CollageWorld/Collageworld/ImageWorld/Assets/SavedInfo/" + $"/{worldName}/" + i.IMAGEID.ToString() + ".png", savedImage);
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
