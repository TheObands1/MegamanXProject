using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    GameObject[] enemies;
    GameObject[] player;
    public Text enemyCount;
    public Win WinScreen;
    public GameOver gameOver;
    public string time;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        winCondition();
        gameOverCondition();    
       
    }

    private void winCondition()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount.text = "Enemies left: " + GetEnemiesLeft().ToString();
        if (GetEnemiesLeft() == 0)
        {
            WinScreen.Setup(GetEnemiesLeft());
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    private void gameOverCondition()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        if (player.Length == 0)
        {
            gameOver.Setup(); 
        }
       
    }

    public int GetEnemiesLeft()
    {
        return enemies.Length;
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
