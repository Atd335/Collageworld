using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractorScript : MonoBehaviour
{

    public TextMeshPro modeText;

    void Start()
    {
        
    }

    RaycastHit hit;
    public Transform head;

    public static GameObject interactedOBJ;
    public static GameObject hoveredOBJ;

    public Color[] colors;
    public int colorInt = 0;

    public static int interactMode;
    //0 - move
    //1 - delete
    //2 - mask
    //3 - sketchify
    //4 - duplicate
    //5 - flip
    //6 - color

    string[] modes = { "moving","destroy","cut-out","sketchify","duplicate","flip","colors"};

    public Transform iconWheel;

    public SpriteRenderer crosshair;
    public Sprite[] hairs;



    void Update()
    {
        if (PlayerMovement.inMenu || PauseMenuScript.imPaused) { return; }

        if (!Input.GetKey(KeyCode.Mouse0) && Input.GetAxisRaw("Mouse ScrollWheel")!=0)
        {
            interactMode += Mathf.RoundToInt(Mathf.Sign(Input.GetAxisRaw("Mouse ScrollWheel")));
        }
        if (interactMode>6)
        {
            interactMode = 0;
        }
        if (interactMode<0)
        {
            interactMode = 6;
        }
        interactMode = Mathf.Clamp(interactMode,0,6);
        modeText.text = modes[interactMode];

        if (interactMode == 0) //movement
        {
            mover();
        }
        else if (interactMode == 1) //deletion
        {
            remover();
        }
        else if (interactMode == 3)
        {
            sketchify();
        }
        else if (interactMode == 4)
        {
            duplicate();
        }
        else if (interactMode == 5)
        {
            flips();
        }
        else if (interactMode == 6)
        {
            colorChange();
        }
        Camera.main.GetComponent<TextureEditorTest>().enabled = interactMode == 2;

        iconWheel.localRotation = Quaternion.Lerp(iconWheel.localRotation, Quaternion.Euler(0, 0, (360 / 7) * interactMode),Time.deltaTime * 15);
        crosshair.sprite = hairs[interactMode];
    }

    void colorChange()
    {
        if (Physics.Raycast(head.transform.position, head.forward, out hit))
        {
            if (hit.collider.tag == "interactable")
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    colorInt++;
                    if (colorInt == colors.Length)
                    {
                        colorInt = 0;
                    }
                    hit.collider.gameObject.transform.parent.transform.parent.GetComponent<TextureHavenScript>().changeColor(colors[colorInt]);
                }
                else if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    colorInt--;
                    if (colorInt == -1)
                    {
                        colorInt = colors.Length-1;
                    }
                    hit.collider.gameObject.transform.parent.transform.parent.GetComponent<TextureHavenScript>().changeColor(colors[colorInt]);
                }
            }
        }
    }

    void flips()
    {
        if (Physics.Raycast(head.transform.position, head.forward, out hit))
        {
            if (hit.collider.tag == "interactable")
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    hit.collider.gameObject.transform.parent.transform.parent.Rotate(0,180,0);
                }
                else if(Input.GetKeyDown(KeyCode.Mouse1))
                {
                    hit.collider.gameObject.transform.parent.transform.parent.Rotate(180,0, 0);
                }
            }
        }
    }

    void duplicate()
    {
        if (Physics.Raycast(head.transform.position, head.forward, out hit))
        {
            if (hit.collider.tag == "interactable")
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Instantiate(hit.collider.gameObject.transform.parent.transform.parent.gameObject,PlayerMovement.spawnLoc.position - PlayerMovement.spawnLoc.forward,PlayerMovement.spawnLoc.rotation);
                }
            }
        }
    }

    void hover()
    {
        if (Physics.Raycast(head.transform.position, head.forward, out hit))
        {
            if (hit.collider.tag == "interactable")
            {
                hoveredOBJ = hit.collider.gameObject.transform.parent.transform.parent.gameObject;
            }
            else
            {
                hoveredOBJ = null;
            }
        }
        else
        {
            hoveredOBJ = null;
        }
    }

    void remover()
    {
        if (Physics.Raycast(head.transform.position, head.forward, out hit))
        {
            if (hit.collider.tag == "interactable")
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    DelObj(hit.collider.gameObject.transform.parent.transform.parent.gameObject);
                }
            }
        }
    }

    void sketchify()
    {
        if (Physics.Raycast(head.transform.position, head.forward, out hit))
        {
            if (hit.collider.tag == "interactable")
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    hit.collider.gameObject.GetComponentInParent<SketchifyItem>().enabled = !hit.collider.gameObject.GetComponentInParent<SketchifyItem>().enabled;
                    hit.collider.transform.parent.localScale = Vector3.one;
                    hit.collider.transform.parent.localRotation = Quaternion.Euler(Vector3.zero);
                }
            }
        }
    }

    void DelObj(GameObject obj)
    {
        Destroy(obj);
    }

    void mover()
    {
        if (Physics.Raycast(head.transform.position, head.forward, out hit))
        {
            if (hit.collider.tag == "interactable")
            {           
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (interactedOBJ == null)
                    {
                        interactedOBJ = hit.collider.gameObject.transform.parent.transform.parent.gameObject;
                    }
                    if (interactedOBJ.GetComponent<ImageSyncer>())
                    {
                        interactedOBJ.GetComponent<ImageSyncer>().UpdatePos();
                    }
                    //Debug.Log("HEY HEY HEY");
                }
                else
                {
                    interactedOBJ = null;
                }
            }
            else
            {
                interactedOBJ = null;
            }
        }
        else
        {
            interactedOBJ = null;
        }
    }
}
