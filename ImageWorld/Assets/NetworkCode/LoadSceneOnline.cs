using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LoadSceneOnline : NetworkBehaviour
{
    void Start()
    {
        if (NetworkServer.active)
        {
            Debug.Log(WorldNamePasser.worldName);
            GameObject.Find("SAVE/LOAD STAGE").GetComponent<LoadSave>().LoadWorld();
        }
    }
}
