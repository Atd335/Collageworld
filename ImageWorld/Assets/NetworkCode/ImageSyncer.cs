using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ImageSyncer : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdatePos()
    {
        CmdUpdatePos(transform.position);
    }

    [Command(ignoreAuthority = true)]
    void CmdUpdatePos(Vector3 pos)
    {
        RpcUpdatePos(pos);
    }
    [ClientRpc]
    void RpcUpdatePos(Vector3 pos)
    {
        transform.position = pos;
    }

}
