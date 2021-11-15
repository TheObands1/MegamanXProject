using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    HealthComponent myHealthComponent;
    UI myUI;
    FlyingEnemy myFlyingEnemy;
    StaticEnemy1 myStaticEnemy1;
    StaticEnemy2 myStaticEnemy2;
    public int enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

    // Start is called before the first frame update
    void Start()
    {
        myHealthComponent = GetComponent<HealthComponent>();
        myUI = GetComponent<UI>();
        myFlyingEnemy = GetComponent<FlyingEnemy>();
        myStaticEnemy1 = GetComponent<StaticEnemy1>();
        myStaticEnemy2 = GetComponent<StaticEnemy2>();
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (myFlyingEnemy.GetIsDead() || myStaticEnemy1.GetIsDead() || myStaticEnemy2.GetIsDead())
            enemies -= 1;

        if (enemies == 0)
            myUI.LoadGame();
    }

    public int GetEnemiesLeft()
    {
        return enemies;
    }
}
