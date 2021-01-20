using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TrackImagesInChildren : NetworkBehaviour
{

    public override void OnStartClient()
    {
        //fires every connected client

        base.OnStartClient();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!NetworkServer.active)// when a client who isnt the server joins
        {
            //CmdSendMessage();
        }
    }


    public Texture2D[] texturesInChildren;

    int lastNum = 0;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(NetworkServer.active);
        if (lastNum!=transform.childCount)
        {
            texturesInChildren = new Texture2D[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                texturesInChildren[i] = GetComponentsInChildren<TextureHavenScript>()[i].editTex;
            }
            lastNum = transform.childCount;
        }
    }


}
