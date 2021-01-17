using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public bool interacting;
    public float rotX;
    public int rotZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        interacting = this.gameObject == InteractorScript.interactedOBJ;

        if (interacting)
        {
            transform.parent = Camera.main.transform;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                rotX += Input.GetAxisRaw("Mouse Y");
            }
            transform.rotation = Quaternion.Euler(rotX, transform.rotation.eulerAngles.y, 0);
            transform.localScale += Vector3.one * Input.GetAxisRaw("Mouse ScrollWheel");
            if (transform.localScale.x<.1f)
            {
                transform.localScale = Vector3.one * .1f;
            }
        }
        else
        {
            transform.parent = GameObject.Find("ALL_IMAGES").transform;
        }
        if (PlayerMovement.inMenu || PauseMenuScript.imPaused)
        {
            transform.parent = GameObject.Find("ALL_IMAGES").transform;
        }
        if (transform.position.y < -.9f) { transform.position = new Vector3(transform.position.x, -.9f, transform.position.z); }
    }
}
