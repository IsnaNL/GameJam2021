using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    public Button firstButt;
    public Button backToMenuButt;
    public GameObject CreditsMenu;
    public GameObject Main;
    // Start is called before the first frame update
    void Start()
    {
        
        //Cursor.lockState = CursorLockMode.Locked;
        BackToMain();
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Credits()
    {
        Main.SetActive(false);
        CreditsMenu.SetActive(true);
        backToMenuButt.Select();
    }
    public void BackToMain()
    {
        Main.SetActive(true);
        CreditsMenu.SetActive(false);
        firstButt.Select();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
