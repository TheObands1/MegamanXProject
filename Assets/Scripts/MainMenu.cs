using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public controls controlsS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void gameQuit()
    {
        Application.Quit();
    }
    public void goToMenu()
    {
        controlsS.Setup(false);
    }
    public void controls()
    {
        controlsS.Setup(true);
    }
}
