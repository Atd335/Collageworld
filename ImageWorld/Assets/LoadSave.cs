using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSave : MonoBehaviour
{
    public bool LoadOnPlay;
    public string worldToLoad;

    public GameObject spawnedMesh;

    private void Awake()
    {
        worldToLoad = WorldNamePasser.worldName;
        if (WorldNamePasser.isLoading && SceneManager.GetActiveScene().buildIndex==2) { LoadWorld(); }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!LoadOnPlay) { return; }
        LoadWorld();
        //lines[ LINE + (FREQUENCY * IMAGE INSTANCE) ]
    }

    public void LoadWorld()
    {
        lines = File.ReadAllLines("C:/CollageWorld/SavedInfo/" + $"{worldToLoad}/{worldToLoad}.txt");

        numOfImages = int.Parse(lines[0].ToString()); //the number of images to spawn
        print(numOfImages);
        loadImages();
    }

    string[] lines;

    public int numOfImages;

    string imageID;
    Texture2D loadedTex;
    Vector3 loadedPos;
    Vector3 loadedRot;
    Vector3 loadedScale;
    Color loadedColor;
    bool loadedSketch;

    void loadImages()
    {
        for (int i = 0; i < numOfImages; i++)
        {
            print(lines[2 + (12 * i)]); //ID NUMBER
            //name
            imageID = lines[2 + (12 * i)];
            //tex
            loadedTex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            loadedTex.LoadImage(File.ReadAllBytes("C:/CollageWorld/SavedInfo/" + $"{worldToLoad}/{imageID}.png"));
            //pos
            loadedPos = new Vector3(float.Parse(lines[4 + (12 * i)].Split(',')[0]), float.Parse(lines[4 + (12 * i)].Split(',')[1]), float.Parse(lines[4 + (12 * i)].Split(',')[2]));
            //rot
            loadedRot = new Vector3(float.Parse(lines[6 + (12 * i)].Split(',')[0]), float.Parse(lines[6 + (12 * i)].Split(',')[1]), float.Parse(lines[6 + (12 * i)].Split(',')[2]));
            //scale
            loadedScale = new Vector3(float.Parse(lines[8 + (12 * i)].Split(',')[0]), float.Parse(lines[8 + (12 * i)].Split(',')[1]), float.Parse(lines[8 + (12 * i)].Split(',')[2]));
            //color
            loadedColor = new Color(float.Parse(lines[10 + (12 * i)].Split(',')[0]), float.Parse(lines[10 + (12 * i)].Split(',')[1]), float.Parse(lines[10 + (12 * i)].Split(',')[2]), float.Parse(lines[10 + (12 * i)].Split(',')[3]));
            //sketched
            loadedSketch = lines[12] == "T";

            SpawnIt(loadedTex, loadedPos, loadedRot, loadedScale, loadedColor, loadedSketch);

            //SPAWN THE OBJECT
        }

        Debug.Log("WorldLoaded");
    }

    public void SpawnIt(Texture2D tex, Vector3 pos, Vector3 rot, Vector3 scale, Color color, bool sketched)
    {

        //create the gameobject
        GameObject i = Instantiate(spawnedMesh, pos, Quaternion.Euler(rot));
        i.GetComponentsInChildren<MeshRenderer>()[0].material.mainTexture = tex;
        i.GetComponentsInChildren<MeshRenderer>()[1].material.mainTexture = tex;
        i.GetComponent<TextureHavenScript>().CreateImage();
        i.GetComponent<TextureHavenScript>().changeColor(color);
        i.GetComponentInChildren<SketchifyItem>().enabled = sketched;
        i.transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
