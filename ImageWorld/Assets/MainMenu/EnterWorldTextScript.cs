using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnterWorldTextScript : MonoBehaviour
{
    MainMenuButtonScript button;
    public TextMeshPro enteredText;
    public TextMeshPro placeHolderText;

    public static string worldNameString; 

    void Start()
    {
        worldNameString = "";
        button = GetComponent<MainMenuButtonScript>();
    }

    void Update()
    {
        if (button.isActivated)
        {
            transform.localScale = button.initScale;

            foreach (KeyCode vKey in Enum.GetValues(typeof(KeyCode)))
            {
                if (vKey.ToString().Length == 1 || vKey.ToString() == "Space")
                {
                    if (Char.IsLetterOrDigit(vKey.ToString(), 0) || vKey.ToString() == "Space")
                    {
                        if (Input.GetKeyDown(vKey) && worldNameString.Length < 34)
                        {
                            if (vKey.ToString() != "Space")
                            {
                                worldNameString += vKey.ToString();
                            }
                            else
                            {
                                worldNameString += " ";
                            }

                        }
                    }
                }
                else if (vKey == KeyCode.Backspace)
                {
                    if (Input.GetKeyDown(vKey) && worldNameString.Length > 0)
                    {
                        //print("AAAA");
                        worldNameString = worldNameString.Remove(worldNameString.Length - 1);
                    }
                }
            }

            if (MainMenuMouseInputs.activeScreenPos!=1 || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) { button.isActivated = false; }
        }

        enteredText.enabled = worldNameString != "";
        enteredText.text = worldNameString;
        WorldNamePasser.worldName = worldNameString;
        placeHolderText.enabled = worldNameString == "";
    }
}
