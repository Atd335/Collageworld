using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButtonScript : MonoBehaviour
{
    PauseMenuButtonScript PMBS;

    // Start is called before the first frame update
    void Start()
    {
        PMBS = GetComponent<PauseMenuButtonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (PMBS.isActivated)
        {
            GameObject.Find("SAVE/LOAD STAGE").GetComponent<SaveStage>().SaveWorld();
            PMBS.isActivated = false;
        }
    }
}
