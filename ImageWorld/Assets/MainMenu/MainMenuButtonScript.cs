using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonScript : MonoBehaviour
{

    public bool isHovered;
    public bool isActivated;

    public Vector3 initScale;
    Vector3 selScale;

    public float bigScale = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        initScale = transform.localScale;
        selScale = initScale * bigScale;
    }

    // Update is called once per frame
    void Update()
    {
        isHovered = MainMenuMouseInputs.hoveredOBJ == this.gameObject;

        if (isHovered)
        {
            if (isActivated) { return; }
            transform.localScale = Vector3.Lerp(transform.localScale, selScale, Time.deltaTime * 16);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, initScale, Time.deltaTime * 16);
        }
    }
}
