using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TextureHavenScript : MonoBehaviour
{
    public Texture2D editTex;
    public Texture2D refTex;
    public MeshRenderer[] MRs;
    Vector2 texSize;

    public string url;

    public int IMAGEID;

    public Color currentColor;

    public SketchifyItem SI;
    public bool sketchified;

    // Start is called before the first frame update
    public void CreateImage()
    {
        Texture2D tex = MRs[0].material.mainTexture.ToTexture2D();
        tex.name = "objTex";
        editTex = new Texture2D(tex.width, tex.height);

        RenderTexture currentRT = RenderTexture.active;

        RenderTexture renderTexture = new RenderTexture(tex.width, tex.height, 32);

        Graphics.Blit(tex, renderTexture);

        RenderTexture.active = renderTexture;

        editTex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texSize = new Vector2(editTex.width, editTex.height);
        MRs[0].transform.localScale = new Vector3(texSize.x / texSize.y, 1, 1) * 4f;
        MRs[1].transform.localScale = new Vector3(texSize.x / texSize.y, 1, -1) * 4f;
        editTex.Apply();

        Sprite spr = Sprite.Create(editTex, new Rect(0, 0, editTex.width, editTex.height), Vector2.one * .5f);
        GetComponentsInChildren<SpriteRenderer>()[0].sprite = spr;
        GetComponentsInChildren<SpriteRenderer>()[1].sprite = spr;
        float resizeFactor = (spr.texture.width / 100f) / ((texSize.x / texSize.y) * 4);
        float test = 1 / resizeFactor;
        GetComponentsInChildren<SpriteRenderer>()[0].transform.localScale = Vector3.one * test;
        GetComponentsInChildren<SpriteRenderer>()[1].transform.localScale = new Vector3(1,1,-1) * test;
        UpdateColliders();

        refTex = new Texture2D(editTex.width,editTex.height);
        refTex.ReadPixels(new Rect(0, 0, editTex.width, editTex.height), 0, 0);
        refTex.Apply();
    }

    void CreateTexStepTwo()
    {

    }

    private void Awake()
    {
        IMAGEID = this.gameObject.GetInstanceID();
        if (editTex)
        {
            CreateImage();
        }

        currentColor = Color.white;
    }

    public void UpdateTex()
    {
        MRs[0].material.mainTexture = editTex;
        MRs[1].material.mainTexture = editTex;
    }

    public GameObject[] sprMesh;

    public void UpdateColliders()
    {
        Sprite spr = Sprite.Create(editTex, new Rect(0, 0, editTex.width, editTex.height), Vector2.one * .5f);
        GetComponentsInChildren<SpriteRenderer>()[0].sprite = spr;
        GetComponentsInChildren<SpriteRenderer>()[1].sprite = spr;

        Destroy(sprMesh[0].GetComponent<MeshCollider>());
        sprMesh[0].GetComponent<MeshFilter>().mesh = SpriteToMesh(GetComponentsInChildren<SpriteRenderer>()[0].sprite);
        sprMesh[0].AddComponent<MeshCollider>();

        Destroy(sprMesh[1].GetComponent<MeshCollider>());
        sprMesh[1].GetComponent<MeshFilter>().mesh = SpriteToMesh(GetComponentsInChildren<SpriteRenderer>()[1].sprite);
        sprMesh[1].AddComponent<MeshCollider>();
        Debug.Log("MESH UPDATED!");
    }

    public MeshRenderer highlight;
    private void Update()
    {
        sketchified = SI.enabled;
        //highlight.material.SetColor("_Color", new Color(highlight.material.color.r, highlight.material.color.g, highlight.material.color.b,1));
    }

    public void changeColor(Color col)
    {
        MRs[0].material.SetColor("_Color", col);
        MRs[1].material.SetColor("_Color", col);
        currentColor = col;
    }

    Mesh SpriteToMesh(Sprite sprite)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = Array.ConvertAll(sprite.vertices, i => (Vector3)i);
        mesh.uv = sprite.uv;
        mesh.triangles = Array.ConvertAll(sprite.triangles, i => (int)i);

        return mesh;
    }
}
