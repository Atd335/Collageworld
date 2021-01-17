using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToMenuScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PauseMenuButtonScript>().isActivated)
        {
            Destroy(GameObject.Find("WorldNamePasser"));
            SceneManager.LoadScene(0);
        }
    }
}
