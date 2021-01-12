using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuControls : MonoBehaviour
{
    public SpriteRenderer DARKEN;
    public Transform searchTextTransform;
    public Transform imageResults;

    public string searchText;
    public TextMeshPro text;

    public Camera imageCam;

    // Start is called before the first frame update
    void Start()
    {
        GG = GetComponent<GetImageFromGoogle>();
    }

    // Update is called once per frame
    void Update()
    {
        toggleVisuals();
        KeyInputs();
        mouseInputs();
        text.text = searchText;
    }

    public static GameObject hoveredObj;

    void mouseInputs()
    {
        if (PlayerMovement.inMenu)
        {
            Ray ray = imageCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.tag == "image" && hit.collider.gameObject.GetComponent<MeshRenderer>().material.color.a == 1)
                {
                    hoveredObj = hit.collider.gameObject;
                    //Debug.Log(hit.collider.gameObject.name);
                    if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        hoveredObj.GetComponent<menuInteractScript>().SpawnIt();
                        PlayerMovement.inMenu = false;
                    }
                }
                else
                {
                    hoveredObj = null;
                }
            }
            else
            {
                hoveredObj = null;
            }
        }
        else
        {

        }
    }

    void KeyInputs()
    {
        if (PlayerMovement.inMenu)
        {
            foreach (KeyCode vKey in Enum.GetValues(typeof(KeyCode)))
            {
                if (vKey.ToString().Length == 1 || vKey.ToString() == "Space")
                {
                    if (Char.IsLetterOrDigit(vKey.ToString(), 0) || vKey.ToString() == "Space")
                    {
                        if (Input.GetKeyDown(vKey) && searchText.Length < 34)
                        {
                            //print(vKey.ToString());
                            if (vKey.ToString() != "Space")
                            {
                                searchText += vKey.ToString();
                            }
                            else
                            {
                                searchText += " ";
                            }

                        }
                    }
                }
                else if (vKey == KeyCode.Backspace)
                {
                    if (Input.GetKeyDown(vKey) && searchText.Length>0)
                    {
                        //print("AAAA");
                        searchText = searchText.Remove(searchText.Length-1);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                EnterInput(searchText);
            }
        }
        else
        {
            searchText = "";
        }
    }

    GetImageFromGoogle GG;

    void EnterInput(string search)
    {
        //input search
        GG.querey = searchText;
        GG.SearchIt();
        searchText = "";
    }

    public SpriteRenderer logo;

    void toggleVisuals()
    {
        if (PlayerMovement.inMenu)
        {
            logo.color = Color.Lerp(logo.color,Color.white,Time.deltaTime * 15);
            DARKEN.color = Color.Lerp(DARKEN.color, new Color(0, 0, 0, 0.6f), Time.deltaTime * 18);
            searchTextTransform.localPosition = Vector3.Lerp(searchTextTransform.localPosition, new Vector3(1.25f,-4.3f,1),Time.deltaTime * 15);
            imageResults.localPosition = Vector3.Lerp(imageResults.localPosition,Vector3.zero,Time.deltaTime * 10);
        }
        else
        {
            logo.color = Color.Lerp(logo.color, new Color(1,1,1,0), Time.deltaTime * 15);
            DARKEN.color = Color.Lerp(DARKEN.color, new Color(0, 0, 0, 0f), Time.deltaTime * 18);
            searchTextTransform.localPosition = Vector3.Lerp(searchTextTransform.localPosition, new Vector3(1.25f, -6.4f, 1), Time.deltaTime * 15);
            imageResults.localPosition = Vector3.Lerp(imageResults.localPosition, new Vector3(0,8f,0), Time.deltaTime * 10);
            if (imageResults.localPosition.y>=7.1f)
            {
                foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
                {
                    if (m.name[0]=='Q')
                    {
                        m.GetComponent<menuInteractScript>().url = "";
                        m.material.SetTexture("_MainTex", null);
                        m.material.SetColor("_Color", new Color(1, 1, 1, .1f));
                    }
                }
            }
        }
    }
}
