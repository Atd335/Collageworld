using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController CC;

    public Transform head;
    public Transform headMast;
    public float speed;
    public Vector3 moveDirection;
    public Vector3 moveDirectionDone;
    public float xRot;
    public float yRot;
    public float mouseSens;

    public float grav;
    public float jumpHeight;
    public bool isGrounded;

    public static bool inMenu;
    public static Transform spawnLoc;

    public bool cinematicView;
    public Camera normCam;
    public Camera uiCam;

    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CharacterController>();
        spawnLoc = GameObject.Find("SpawnLoc").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (inMenu || PauseMenuScript.imPaused)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F1)) { cinematicView = !cinematicView; }
            Cursor.lockState = CursorLockMode.Locked;
        }

        pauseInputs();

        headRotations();
        keyInputs();


        if (cinematicView)
        {
            uiCam.depth = -1;
            normCam.depth = 1;
        }
        else
        {
            uiCam.depth = 1;
            normCam.depth = -1;
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            moveDirection.y = -1f;
            if (Input.GetKey(KeyCode.Space) && !inMenu && !PauseMenuScript.imPaused)
            {
                moveDirection.y = jumpHeight;
            }
        }
        else
        {
            moveDirection.y -= grav;
        }

        //moveDirection = Vector3.ClampMagnitude(moveDirection,1);
        moveDirection = moveDirection.x * headMast.right + moveDirection.z * headMast.forward + moveDirection.y * headMast.up;

        moveDirectionDone = moveDirection;
        moveDirectionDone.x *= speed;
        moveDirectionDone.z *= speed;

        CC.Move(moveDirectionDone * Time.deltaTime);
    }

    void pauseInputs()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !PauseMenuScript.imPaused)
        {
            inMenu = !inMenu;
        }
    }

    void headRotations()
    {
        if (!inMenu && !PauseMenuScript.imPaused && !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftAlt))
        {
            xRot -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSens;
            yRot += Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSens;
        }

        xRot = Mathf.Clamp(xRot,-89,89);

        head.localRotation = Quaternion.Euler(xRot,yRot,0);
        headMast.localRotation = Quaternion.Euler(0,head.localRotation.eulerAngles.y,0);
    }

    void keyInputs()
    {
        isGrounded = CC.isGrounded;
        if (!inMenu && !PauseMenuScript.imPaused)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
        }
        else
        {
            moveDirection = new Vector3(0,moveDirection.y,0);
        }

    }
}
