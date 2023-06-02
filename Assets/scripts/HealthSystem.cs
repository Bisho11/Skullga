using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour

{
    public float health = 3;
   

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log(health);
        animator.SetTrigger("damage");
        

        if (health <= 0)
        {
            animator.SetTrigger("death");
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
   
}
