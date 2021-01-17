using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{

    public static bool imPaused; 
    public SpriteRenderer[] bgs;
    public SpriteRenderer[] MENUOPTIONS;

    Camera pauseCam;

    // Start is called before the first frame update
    void Start()
    {
        pauseCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { imPaused = !imPaused; }
        PauseMenuVisuals();
        PauseMenuInteract();
    }

    public static GameObject hoveredOBJ;

    void PauseMenuInteract()
    {
        if (imPaused)
        {
            RaycastHit2D hit = Physics2D.Raycast(pauseCam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,-1)), Vector2.zero);

            if (hit.collider != null && hit.collider.tag == "PauseMenu")
            {
                hoveredOBJ = hit.collider.gameObject;
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

    void PauseMenuVisuals()
    {
        if (imPaused)
        {
            foreach (SpriteRenderer s in bgs)
            {
                s.color = Color.Lerp(s.color, new Color(s.color.r, s.color.g, s.color.b, .3f), Time.deltaTime * 18);
            }

            foreach (SpriteRenderer s in MENUOPTIONS)
            {
                s.color = Color.Lerp(s.color, new Color(s.color.r, s.color.g, s.color.b, 1f), Time.deltaTime * 18);
            }
        }
        else
        {
            foreach (SpriteRenderer s in bgs)
            {
                s.color = Color.Lerp(s.color, new Color(s.color.r, s.color.g, s.color.b, 0), Time.deltaTime * 18);
            }

            foreach (SpriteRenderer s in MENUOPTIONS)
            {
                s.color = Color.Lerp(s.color, new Color(s.color.r, s.color.g, s.color.b, 0f), Time.deltaTime * 18);
            }
        }
    }
}
