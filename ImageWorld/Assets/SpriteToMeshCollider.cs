using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;

public class SpriteToMeshCollider : MonoBehaviour
{


    public SpriteRenderer SR;

    public Mesh sprMesh;
    public Mesh sprMesh2;

    public GameObject holder;

    public Texture2D referenceTexture;

    public string url;

    // Start is called before the first frame update
    public void CreateCollider()
    {
        StartCoroutine(DownloadImage(url));

        createMask();

       //create front mesh, then add a collider
        sprMesh = SpriteToMesh(SR.sprite);
        sprMesh.name = "front mesh";
        GameObject g = Instantiate(holder,transform);
        g.name = "front collider";
        g.GetComponent<MeshFilter>().mesh = sprMesh;
        g.AddComponent<MeshCollider>();
        g.transform.localPosition = new Vector3(0,0,-.01f);

        // create a back mesh, then add a collider
        sprMesh2 = SpriteToMesh(SR.sprite);
        sprMesh2.triangles = sprMesh2.triangles.Reverse().ToArray();
        GameObject gg = Instantiate(holder, transform);
        sprMesh2.name = "back mesh";
        gg.name = "back collider";
        gg.GetComponent<MeshFilter>().mesh = sprMesh2;
        gg.AddComponent<MeshCollider>();
        gg.transform.localPosition = new Vector3(0, 0, .01f);
    }

    void createMask()
    {

    }

    public void UpdateColliders()
    {

    }

    Mesh SpriteToMesh(Sprite sprite)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = Array.ConvertAll(sprite.vertices, i => (Vector3)i);
        mesh.uv = sprite.uv;
        mesh.triangles = Array.ConvertAll(sprite.triangles, i => (int)i);

        return mesh;
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            referenceTexture = ((DownloadHandlerTexture)request.downloadHandler).texture.ToTexture2D();
    }
}
