using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;

public class ConnectIPScript : MonoBehaviour
{
    public string inputString;

    public string IP;
    public string PORT;

    public NetworkManager NM;
    public NetworkManagerHUD NMH;
    public TelepathyTransport TT;

    public TMP_InputField inputField;

    public Transform IPUI;

    // Start is called before the first frame update
    void Start()
    {
        NM = GameObject.Find("HUD").GetComponent<NetworkManager>();
        TT = GameObject.Find("HUD").GetComponent<TelepathyTransport>();
    }

    // Update is called once per frame
    void Update()
    {

        inputString = inputField.text;

        string[] str = inputString.Split(':');
        if (str.Length > 0)
            IP = str[0];
        if (str.Length > 1)
            PORT = str[1];

        int _portInt;
        int portInt;
        if (int.TryParse(PORT, out _portInt))
        {
            portInt = _portInt;
        }
        else
        {
            portInt = 7777;
        }

        if (IP == null || IP == "")
        {
            IP = "localhost";
        }

        NM.networkAddress = IP;
        TT.port = Convert.ToUInt16(portInt);

        if (Camera.main.transform.position.y < -24)
            IPUI.localPosition = Vector3.Lerp(IPUI.localPosition,new Vector3(0,-75,0),Time.deltaTime * 15);
        else
            IPUI.localPosition = Vector3.Lerp(IPUI.localPosition, new Vector3(0, -375, 0), Time.deltaTime * 15);

    }

    public void EnterGame()
    {
        NM.StartClient();
    }
}
