using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour

{
    bool dead = false;
    public float health = 3;
    Character charac;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        charac = GetComponent<Character>();
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log(health);
        if (health > 0)
        {
            animator.SetTrigger("damage");
        }

        else if (!dead)
        {
            dead = true;
            animator.SetTrigger("death");
            charac.DisableComponent();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
   
}
