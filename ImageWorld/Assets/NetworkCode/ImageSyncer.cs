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

        //UpdatePos(localID.netId);
        //if (!NetworkServer.active)
        //{
        //    CmdGrabUrl(localID.netId);
        //}
    }

    public string loadedUrl;

    //upon loading into the server*********
    [Command(ignoreAuthority = true)]
    void CmdGrabUrl(uint filter)
    {
        RpcGrabUrl(filter);
    }
    [ClientRpc]
    void RpcGrabUrl(uint filter)
    {
        if (NetworkServer.active)
        {
            if (loadedUrl == "" || loadedUrl == null)
            {
                loadedUrl = GetComponent<TextureHavenScript>().url;
            }
            CmdSendUrl(loadedUrl, filter);            
        }
    }

    [Command(ignoreAuthority = true)]
    void CmdSendUrl(string url, uint filter)
    {
        RpcSendUrl(url, filter);
    }
    [ClientRpc]
    void RpcSendUrl(string url, uint filter)
    {
        print(url + "|HEY HEY HEY THIS IS THE URL");
        if (filter == localID.netId)
        {
            StartCoroutine(DownloadImage(url));
        }
    }
    //upon loading into ther server*********

    NetworkIdentity localID;


    bool imageSet;
    // Update is called once per frame
    void Update()
    {
        if (!imageSet)
        {
            foreach (NetworkIdentity n in GameObject.Find("WORLD").GetComponentsInChildren<NetworkIdentity>())
            {
                if (n.isLocalPlayer)
                {
                    localID = n;
                }
            }

            if (!NetworkServer.active)
            {
                CmdGrabUrl(localID.netId);
            }
            imageSet = true;
        }

        if (localID.isLocalPlayer && Input.GetKeyDown(KeyCode.J))
        {
            //UpdateImageData();
        }
    }

    public void UpdatePos(uint filter)
    {
        CmdUpdatePos(transform.position,transform.rotation.eulerAngles,transform.localScale,filter);
    }

    [Command(ignoreAuthority = true)]
    void CmdUpdatePos(Vector3 pos,Vector3 rot, Vector3 scl,uint filter)
    {
        RpcUpdatePos(pos,rot,scl,filter);
    }
    [ClientRpc]
    void RpcUpdatePos(Vector3 pos, Vector3 rot, Vector3 scl, uint filter)
    {
        if (localID.netId != filter)
        {
            transform.position = pos;
            transform.localScale = scl;
            transform.rotation = Quaternion.Euler(rot);
        }
    }

    public void ToggleSketch()
    {
        CmdToggleSketch();
    }
    [Command(ignoreAuthority = true)]
    void CmdToggleSketch()
    {
        RpcToggleSketch();
    }
    [ClientRpc]
    void RpcToggleSketch()
    {
        GetComponentInChildren<SketchifyItem>().enabled = !GetComponentInChildren<SketchifyItem>().enabled;
        GetComponentInChildren<SketchifyItem>().transform.localScale = Vector3.one;
        GetComponentInChildren<SketchifyItem>().transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void SetColor(Color col)
    {
        CmdSetColor(col);
    }
    [Command(ignoreAuthority = true)]
    void CmdSetColor(Color col)
    {
        RpcSetColor(col);
    }
    [ClientRpc]
    void RpcSetColor(Color col)
    {
        GetComponent<TextureHavenScript>().changeColor(col);
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

    // IMAGE DATA TRANSFERRENCE

    public void UpdateImageData()
    {
        if (localID.isLocalPlayer)
        {
            byte[] editTexBytes = GetComponent<TextureHavenScript>().editTex.EncodeToPNG();
            CmdUpdateImageData(localID.netId,editTexBytes);
        }
    }
    [Command(ignoreAuthority = true)]
    void CmdUpdateImageData(uint filter,byte[] imageData)
    {
        RpcUpdateImageData(filter, imageData);
    }
    [ClientRpc]
    void RpcUpdateImageData(uint filter,byte[] imageData)
    {
        if (filter!=localID.netId)
        {
            Texture2D tex = new Texture2D(1,1);
            tex.LoadImage(imageData);
            GetComponent<TextureHavenScript>().editTex = tex;
            GetComponent<TextureHavenScript>().UpdateTex();
            GetComponent<TextureHavenScript>().UpdateColliders();
        }
    }
    //

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            Debug.Log("SET!");
        //((DownloadHandlerTexture)request.downloadHandler).texture;
        loadedUrl = MediaUrl;
        GetComponentInChildren<TextureHavenScript>().CreateImageOnline(((DownloadHandlerTexture)request.downloadHandler).texture);
        CmdInitImage();
    }

    [Command(ignoreAuthority = true)]
    void CmdInitImage()
    {
        RpcInitimage();
    }
    [ClientRpc]
    void RpcInitimage()
    {
        if (NetworkServer.active)
        {
            UpdateImageData();
        }
    }
    
}
