using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadEntryInfo : MonoBehaviour
{

    public TextMeshPro worldName;
    public TextMeshPro worldDate;

    MainMenuButtonScript button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<MainMenuButtonScript>();
        FADE = GameObject.Find("FADE").GetComponent<Image>();
    }

    public Image FADE;

    // Update is called once per frame
    void Update()
    {
        if (button.isActivated)
        {
            WorldNamePasser.worldName = worldName.text;
            WorldNamePasser.isLoading = true;
            FADE.color = Color.Lerp(FADE.color, new Color(1, 1, 1, 1.1f), Time.deltaTime * 18);
            Camera.main.GetComponent<AudioSource>().volume = Mathf.Lerp(Camera.main.GetComponent<AudioSource>().volume, -.5f, Time.deltaTime * 20);
            if (FADE.color.a >= 1)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
