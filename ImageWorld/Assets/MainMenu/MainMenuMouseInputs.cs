using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMouseInputs : MonoBehaviour
{
    public static GameObject hoveredOBJ;
    public Vector3[] screenPositions;
    public static int activeScreenPos;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        activeScreenPos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, screenPositions[activeScreenPos],Time.deltaTime * 17f);

        RaycastHit2D hit = Physics2D.Raycast(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, -1)), Vector2.zero);

        if (hit.collider != null && hit.collider.tag == "MainMenu")
        {
            hoveredOBJ = hit.collider.gameObject;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                hoveredOBJ.GetComponent<MainMenuButtonScript>().isActivated = true;
            }
        }
        else
        {
            hoveredOBJ = null;
        }
    }
}
