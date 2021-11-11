using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_2_Bullet_Left : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    [SerializeField] Animator myAnimator;
    [SerializeField] Collision2D collision2D;
    [SerializeField] bool IsLeftBullet;
    private float collisionSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime * collisionSpeed, speed * Time.deltaTime * collisionSpeed, 0));
    }

    public void SetIsLeftBulletFalse()
    {
        IsLeftBullet = false;
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
