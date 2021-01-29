using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public float TypingSpeed;
   
    public IEnumerator Type(int index)
    {
        textDisplay.text = null;
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
        }
    }
}
