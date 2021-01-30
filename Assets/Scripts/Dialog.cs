using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Image UIimage;
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public float TypingSpeed;
    private int index;
    public AudioClip[] hahaClips;
    public AudioSource audioSourceHAHA;
   
    public IEnumerator Type()
    {
        yield return new WaitForSeconds(0.2f);
        textDisplay.text = null;
        UIimage.enabled = true;
        switch (index)
        {
            case 2:
                audioSourceHAHA.PlayOneShot(hahaClips[0]);
                break;
            case 4:
                audioSourceHAHA.PlayOneShot(hahaClips[1]);
                break;
            case 5:
                audioSourceHAHA.PlayOneShot(hahaClips[2]);
                break;
        }
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
            //
            
        }

        if (textDisplay.text == sentences[index])
        {
            index++;
            yield return new WaitForSeconds(3f);
            textDisplay.text = null;
            
            Debug.Log(index);
        }if(textDisplay.text == null)
        {
            UIimage.enabled = false;
        }
    }
}
