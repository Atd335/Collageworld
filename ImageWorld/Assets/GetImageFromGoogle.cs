using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using System.Linq;
using TMPro;
public class GetImageFromGoogle : MonoBehaviour
{
    public string querey;
    string lastQuerey;
    public int resultID;

    public RawImage[] YourRawImage;

    int resultCount = 0;

    public string currentURL;

    public MeshRenderer[] quads;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            //SearchIt();
        }
    }

    public void SearchIt()
    {
        resultID = 0;
        StartCoroutine("GetData");
    }

    IEnumerator GetData()
    {
        UnityWebRequest webReq = new UnityWebRequest();
        webReq.downloadHandler = new DownloadHandlerBuffer();
        webReq.url = "https://pixabay.com/api/?key=19127784-d61e1c1c7d25cb435828df373&q="+querey+ "&image_type=photo&pretty=false&per_page=10";
        //webReq.url = "https://pixabay.com/api/?key=19127784-d61e1c1c7d25cb435828df373&q="+querey+ "&image_type=photo&pretty=false&per_page=10";

        yield return webReq.SendWebRequest();

        string rawJson = Encoding.Default.GetString(webReq.downloadHandler.data);

        string[] items = rawJson.Split(',');


        resultCount = 0;
        for (int i = 8; i < items.Length; i += 24)
        {
            resultCount++;
        }
        if (resultCount == 0)
        {
            print("no results found");
            querey = "question mark";
            SearchIt();
        }
        else
        {
            Debug.Log(resultCount);

            int strLength = items[11 + (24 * resultID)].Length - 17;
            string newURLString = items[11 + (24 * resultID)].Substring(16, strLength);
            //int strLength = items[8 + (24 * resultID)].Length - 15;
            //string newURLString = items[8 + (24 * resultID)].Substring(14, strLength);

            currentURL = newURLString;
            StartCoroutine(DownloadImage(newURLString));
        }
    }


    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            //Debug.Log("SET!");
            //YourRawImage[0].texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            //YourRawImage[0].SetNativeSize();
            quads[resultID].GetComponent<menuInteractScript>().url = MediaUrl;
            quads[resultID].material.SetTexture("_MainTex", ((DownloadHandlerTexture)request.downloadHandler).texture);
            quads[resultID].material.SetColor("_Color",new Color(1,1,1,1));
            quads[resultID].transform.localScale = new Vector3(3.5f,0,1);

        if (resultID < resultCount - 1)
        {
            resultID++;
            StartCoroutine("GetData");
        }
        else
        {
            for (int i = resultID+1; i < 10; i++)
            {
                quads[resultID].GetComponent<menuInteractScript>().url = "";
                quads[i].material.SetTexture("_MainTex", null);
                quads[i].material.SetColor("_Color", new Color(1, 1, 1, .1f));
            }
        }
    }

}
