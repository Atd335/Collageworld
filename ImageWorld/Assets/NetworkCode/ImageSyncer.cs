using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mirror;

public class ImageSyncer : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (NetworkIdentity n in GameObject.Find("WORLD").GetComponentsInChildren<NetworkIdentity>())
        {
            if (n.isLocalPlayer)
            {
                localID = n;
            }
        }
    }

    NetworkIdentity localID;

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdatePos(uint filter)
    {
        CmdUpdatePos(transform.position,filter);
    }

    [Command(ignoreAuthority = true)]
    void CmdUpdatePos(Vector3 pos,uint filter)
    {
        RpcUpdatePos(pos,filter);
    }
    [ClientRpc]
    void RpcUpdatePos(Vector3 pos,uint filter)
    {
        if (localID.netId != filter)
        {
            transform.position = pos;
        }
    }

    public void SpawnTex(string url)
    {
        CmdSpawnTex(url);
    }

    [Command(ignoreAuthority = true)]
    void CmdSpawnTex(string url)
    {
        RpcSpawnTex(url);
    }
    [ClientRpc]
    void RpcSpawnTex(string url)
    {
        StartCoroutine(DownloadImage(url));
    }

    public void DestroyMe()
    {
        CmdDestroyMe();
    }
    [Command(ignoreAuthority = true)]
    void CmdDestroyMe()
    {
        RpcDestroyMe();
    }
    [ClientRpc]
    void RpcDestroyMe()
    {
        if (NetworkServer.active)
        {
            print("DESTROYED OBJ");
            NetworkServer.Destroy(this.gameObject);          
        }
    }


    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            Debug.Log("SET!");
        //((DownloadHandlerTexture)request.downloadHandler).texture;
        GetComponentInChildren<TextureHavenScript>().CreateImageOnline(((DownloadHandlerTexture)request.downloadHandler).texture);
    }
}
