using System.Collections;
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
}
