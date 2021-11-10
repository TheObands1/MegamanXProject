using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Animator myAnimator;
    [SerializeField] Collision2D collision2D;
    private float collisionSpeed=1;
    // Start is called before the first frame update
    void Start()
    {
        //Put lifespan on bullet so that it does not exist forever in game
        Destroy(this.gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed*Time.deltaTime*collisionSpeed,0,0));
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {    
        if (collision.gameObject.CompareTag("Foreground"))
        {
            collisionSpeed=0;
            myAnimator.SetBool("Hit",true);
            Destroy(this.gameObject,0.3f);
            
        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            HealthComponent HitEnemy = collision.gameObject.GetComponent<HealthComponent>();
            //DoTheSameForEveryType of enemy
            if (HitEnemy != null)
            {
                HitEnemy.DecreaseHealth();
                collisionSpeed = 0;
                myAnimator.SetBool("Hit", true);
                Destroy(this.gameObject, 0.3f);
            }
        }
        else
        {
            collisionSpeed=1;
        }
    }
    
}
