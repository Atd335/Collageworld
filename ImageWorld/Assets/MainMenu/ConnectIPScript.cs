using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ConnectIPScript : MonoBehaviour
{
    public string IP;
    public string PORT;

    public NetworkManager NM;
    public TelepathyTransport TT;

    // Start is called before the first frame update
    void Start()
    {
        NM = GameObject.Find("HUD").GetComponent<NetworkManager>();
        TT = GameObject.Find("HUD").GetComponent<TelepathyTransport>();
    }

    // Update is called once per frame
    void Update()
    {
        NM.networkAddress = IP;
        TT.port = Convert.ToUInt16(int.Parse(PORT));
    }
}
