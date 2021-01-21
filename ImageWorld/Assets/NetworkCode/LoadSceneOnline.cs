using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LoadSceneOnline : NetworkBehaviour
{
    public override void OnStartClient()
    {
        if (!isLocalPlayer)
        {
            Debug.Log("****A PLAYER HAS JOINED THE SERVER****");
            foreach (Transform t in GetComponentsInChildren<Transform>())
            {
                if (t != transform)
                {
                    t.gameObject.SetActive(false);
                }
            }
        }
        base.OnStartClient();
    }

    void Start()
    {
        transform.parent = GameObject.Find("WORLD").transform;
        if (isLocalPlayer)
        {
            if (NetworkServer.active)//if you are the host, and you are the local player
            {
                GameObject.Find("SAVE/LOAD STAGE").GetComponent<LoadSave>().LoadWorld();
                GameObject.Find("SAVE/LOAD STAGE").GetComponent<SaveStage>().isHost = true;
                
            }
            else //if you are not the host, but you are the local player
            {               
                GameObject.Find("SAVE/LOAD STAGE").GetComponent<SaveStage>().isHost = false;
            }
        }
    }

    public void SpawnIt(string url, Vector3 pos, Vector3 rot)
    {
        //print(url);
        CmdSpawnImage(netId, url, pos, rot);
    }

    [Command(ignoreAuthority = true)]
    void CmdSpawnImage(uint filter, string url, Vector3 pos, Vector3 rot)
    {
        RpcSpawnImage(filter, url, pos, rot);
    }
    [ClientRpc]
    void RpcSpawnImage(uint filter, string url, Vector3 pos, Vector3 rot)
    {
        //StartCoroutine(DownloadImage(url, pos, rot));
        if (NetworkServer.active)
        {
            GameObject i = Instantiate(spawnedMesh, pos, Quaternion.Euler(rot));
            NetworkServer.Spawn(i);
            i.GetComponent<ImageSyncer>().SpawnTex(url);
        }
    }

    public GameObject spawnedMesh;

    IEnumerator DownloadImage(string MediaUrl, Vector3 pos, Vector3 rot)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            Debug.Log("SET!");
        BuildImage(((DownloadHandlerTexture)request.downloadHandler).texture, MediaUrl,pos,rot);
        //YourRawImage[0].texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        //YourRawImage[0].SetNativeSize();
    }

    void BuildImage(Texture _tex, string url, Vector3 pos, Vector3 rot)
    {
        Texture texture = _tex;
        Texture2D tex = texture.ToTexture2D();
        //create the gameobject
        if (NetworkServer.active)
        {
            GameObject i = Instantiate(spawnedMesh, pos, Quaternion.Euler(rot));
            NetworkServer.Spawn(i);
            i.GetComponentsInChildren<MeshRenderer>()[0].material.mainTexture = tex;
            i.GetComponentsInChildren<MeshRenderer>()[1].material.mainTexture = tex;
            i.GetComponent<TextureHavenScript>().CreateImage();
            i.GetComponent<TextureHavenScript>().url = url;
        }
    }

}
