using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField]Animator myAnimator;
    [SerializeField] Collision2D collision2D;
    private float colision=1;
    private bool isColision = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        transform.Translate(new Vector3(speed*Time.deltaTime*colision,0,0));
           
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
    
        if (collision.gameObject.CompareTag("Foreground"))
        {
            colision=0;
            myAnimator.SetBool("Hit",true);
            Destroy(this.gameObject,0.2f);
            
            
            
        }
        else
        {
            colision=1;
        }
    }
    
}
