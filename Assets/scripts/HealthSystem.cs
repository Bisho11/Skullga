using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour

{
    [SerializeField] float health = 5;
    [SerializeField] GameObject ragdoll;
    


    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("damage");
        

        if (health <= 0)
        {
            
            Instantiate(ragdoll, transform.position, transform.rotation);
            Die();
        }
    }

    void Die()
    {
        
        Destroy(this.gameObject);
    }
   
}
