using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaneMainMenu : MonoBehaviour
{
    public int ScreenPos;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<MainMenuButtonScript>();
    }

    MainMenuButtonScript button;

    // Update is called once per frame
    void Update()
    {
        if (button.isActivated)
        {
            MainMenuMouseInputs.activeScreenPos = ScreenPos;
            transform.localScale = button.initScale;
            button.isActivated = false;
        }
    }
}
