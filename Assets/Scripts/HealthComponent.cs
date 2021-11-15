using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] float Health;
    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }   

    // Update is called once per frame
    void Update()
    {
        CheckIfObjectIsDead();
    }

    public void CheckIfObjectIsDead()
    {
        if (Health <= 0)
        {
            myAnimator.SetBool("IsDead", true);
            Destroy(this.gameObject, 0.85f);
        }
    }

    public void DecreaseHealth()
    {
        Health--;
    }

    public float GetHealth()
    {
        return Health;
    }
}
