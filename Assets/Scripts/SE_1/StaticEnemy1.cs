using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy1 : MonoBehaviour
{
    [SerializeField] GameObject EnemyBullet;
    [SerializeField] float TimeBetweenFires;
    float currentFireTime;
    float SpriteSizeInX;
    bool isEnemyNear;
    HealthComponent myHealthComponent;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        SpriteSizeInX = GetComponent<SpriteRenderer>().bounds.size.x;
        myHealthComponent = GetComponent<HealthComponent>();
        isEnemyNear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnemyNear)
        {
            FireBullet();
        }
        CheckIfDead();

    }

    void CheckIfDead()
    {
        if (myHealthComponent.GetHealth() <= 0)
        {
            isDead = true;
        }
    }

    public void FireBullet()
    {
        if(Time.time > currentFireTime)
        {
            currentFireTime = Time.time + TimeBetweenFires;
            Instantiate(EnemyBullet, transform.position - new Vector3(SpriteSizeInX, 0, 0), transform.rotation);
        }
    }

    public void SetIsEnemyNear(bool newState)
    {
        isEnemyNear = newState;
    }

    public bool GetIsDead()
    {
        return isDead;
    }
}
