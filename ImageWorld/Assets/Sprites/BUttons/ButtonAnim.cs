using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnim : MonoBehaviour
{
    public Sprite[] buttonSprites;
    SpriteRenderer SR;

    MainMenuButtonScript button;

    // Start is called before the first frame update
    void Start()
    {
        button = transform.parent.transform.parent.GetComponent<MainMenuButtonScript>();
        SR = GetComponent<SpriteRenderer>();
        currentFr = Random.Range(0,buttonSprites.Length);
        animSpd = 10;
    }

    int frames;
    int currentFr;

    float animSpd;



    private void Update()
    {
        if (!button) { return; }

        if (button.isHovered)
        {
            transform.parent.GetComponent<SketchifyItem>().sketchSpd = 2;
            animSpd = 4;
        }
        else
        {
            transform.parent.GetComponent<SketchifyItem>().sketchSpd = 20;
            animSpd = 10;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        frames++;
        if (frames%animSpd==0)
        {
            currentFr++;
        }
        if (currentFr==buttonSprites.Length)
        {
            currentFr = 0;
        }

        SR.sprite = buttonSprites[currentFr];

    }
}
