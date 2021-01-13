using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mirror;

public class menuInteractScript : NetworkBehaviour
{
    public NetworkIdentity NI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public string url;

    // Update is called once per frame
    void Update()
    {
        if (MenuControls.hoveredObj == this.gameObject)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 3.5f, Time.deltaTime * 7);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 3f, Time.deltaTime * 7);
        }
    }

    public GameObject spawnedMesh;

    //public void SpawnIt()
    //{
    //    Texture texture = GetComponent<MeshRenderer>().material.mainTexture;
    //    Texture2D tex = texture.ToTexture2D();
    //    //create the gameobject
    //    GameObject i = Instantiate(spawnedMesh,PlayerMovement.spawnLoc.position,PlayerMovement.spawnLoc.rotation);
    //    i.GetComponentsInChildren<MeshRenderer>()[0].material.mainTexture = tex;
    //    i.GetComponentsInChildren<MeshRenderer>()[1].material.mainTexture = tex;
    //    i.GetComponent<TextureHavenScript>().CreateImage();
    //}

    public void SpawnIt()
    {
        CmdSpawnImage(url);
    }

    [Command(ignoreAuthority = true)]
    void CmdSpawnImage(string _url)
    {
        RpcSpawnImage(_url);
    }

    void RpcSpawnImage(string _url)
    {
        if (NetworkServer.active)
        {
            StartCoroutine(DownloadImage(_url));
        }
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            //((DownloadHandlerTexture)request.downloadHandler).texture)
            Debug.Log("");
    }
}

public static class TextureExtentions
{
    public static Texture2D ToTexture2D(this Texture texture)
    {
        return Texture2D.CreateExternalTexture(
            texture.width,
            texture.height,
            TextureFormat.RGB24,
            false, false,
            texture.GetNativeTexturePtr());
    }
}
