using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateNewWorldScene : MonoBehaviour
{
    MainMenuButtonScript button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<MainMenuButtonScript>();
    }

    public Image FADE;

    // Update is called once per frame
    void Update()
    {
        if (WorldNamePasser.worldName != "" && WorldNamePasser.worldName != null)
        {
            if (button.isActivated)
            {
                FADE.color = Color.Lerp(FADE.color, new Color(1, 1, 1, 1.1f), Time.deltaTime * 18);
                Camera.main.GetComponent<AudioSource>().volume = Mathf.Lerp(Camera.main.GetComponent<AudioSource>().volume, -.5f, Time.deltaTime * 20);
                if (FADE.color.a >= 1)
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
        else
        {
            if (button.isActivated)
            {
                transform.localScale = button.initScale;
                button.isActivated = false;
            }
        }
    }
}
