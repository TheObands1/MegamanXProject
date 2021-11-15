using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class UI : MonoBehaviour
{

    public TMP_Text enemiesText;
    GameController myGameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetUp()
    {
        enemiesText.text = "Enemies Left: " + myGameController.GetEnemiesLeft().ToString();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("WinScene");
    }

}
