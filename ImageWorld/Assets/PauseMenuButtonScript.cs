using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButtonScript : MonoBehaviour
{
    public bool isHovered;

    // Start is called before the first frame update
    void Start()
    {
        initScale = transform.localScale;
        selScale = initScale * 1.1f;
    }

    Vector3 initScale;
    Vector3 selScale;

    // Update is called once per frame
    void Update()
    {
        isHovered = PauseMenuScript.hoveredOBJ == this.gameObject;
        if (isHovered)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, selScale, Time.deltaTime * 6);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, initScale, Time.deltaTime * 6);
        }
    }
}
