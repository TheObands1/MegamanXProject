using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy2 : MonoBehaviour
{
    [SerializeField] GameObject LeftBullet;
    [SerializeField] GameObject RightBullet;
    [SerializeField] float TimeBetweenFires;
    [SerializeField] float rangeOfDetection;
    [SerializeField] AudioClip sfx_Death;
    Animator myAnimator;
    float currentFireTime;
    float SpriteSizeInX;
    float SpriteSizeInY;
    HealthComponent myHealthComponent;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        SpriteSizeInX = GetComponent<SpriteRenderer>().bounds.size.x;
        SpriteSizeInY = GetComponent<SpriteRenderer>().bounds.size.y;
        myHealthComponent = GetComponent<HealthComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, rangeOfDetection, LayerMask.GetMask("Player")) != null)
        {
            myAnimator.SetBool("isSeeingPlayer", true);
            FireBullets();
        }
        else
        {
            myAnimator.SetBool("isSeeingPlayer", false);
        }

        CheckIfDead();
    }

    void CheckIfDead()
    {
        if (myHealthComponent.GetHealth() <= 0)
        {
            isDead = true;
            AudioSource.PlayClipAtPoint(sfx_Death, Camera.main.transform.position);
        }
    }

    public void FireBullets()
    {
        if (Time.time > currentFireTime)
        {
            currentFireTime = Time.time + TimeBetweenFires;
            Instantiate(LeftBullet, transform.position + new Vector3(-SpriteSizeInX+2.7f, SpriteSizeInY-2, 0), transform.rotation);
            
            Instantiate(RightBullet, transform.position + new Vector3(SpriteSizeInX-2.7f, SpriteSizeInY-2, 0), transform.rotation);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
        Gizmos.DrawSphere(transform.position, rangeOfDetection);
    }

    public bool GetIsDead()
    {
        return isDead;
    }
}
