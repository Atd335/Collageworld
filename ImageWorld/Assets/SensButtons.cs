using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensButtons : MonoBehaviour
{
    PauseMenuButtonScript PMBS;

    // Start is called before the first frame update
    void Start()
    {
        PMBS = GetComponent<PauseMenuButtonScript>();   
    }

    public int increment;

    // Update is called once per frame
    void Update()
    {
        if (PMBS.isActivated)
        {
            PMBS.PMS.currentSens += increment;

            if (PMBS.PMS.currentSens>PMBS.PMS.sensArray.Length-1) { PMBS.PMS.currentSens = PMBS.PMS.sensArray.Length - 1; }
            if (PMBS.PMS.currentSens<0) { PMBS.PMS.currentSens = 0; }

            PMBS.PM.mouseSens = PMBS.PMS.sensArray[PMBS.PMS.currentSens]/2;
            transform.localScale = PMBS.initScale/1.1f;
            PMBS.isActivated = false;
        }
    }
}
