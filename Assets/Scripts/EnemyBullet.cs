using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Animator myAnimator;
    [SerializeField] Collision2D collision2D;
    private float collisionSpeed = 1;
    bool isStaticEnemy1 = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isStaticEnemy1)
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime * collisionSpeed, 0, 0));
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.activeInHierarchy)
        {
            collisionSpeed = 0;
            myAnimator.SetBool("Hit", true);
            Destroy(this.gameObject, 0.3f);
        }
        else
        {
            collisionSpeed = 1;
        }
    }

}
