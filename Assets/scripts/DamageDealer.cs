using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealtDamage;

    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
    }

    void Update()
    {
        if (canDealDamage)
        {
            Debug.Log("HI");
            int numRays = 5; // Adjust the number of rays as per your requirements
            float raySpacing = weaponLength / (numRays - 1);

            for (int i = 0; i < numRays; i++)
            {
                Vector3 rayOrigin = transform.position + (transform.forward * (i * raySpacing));

                RaycastHit hit;
                int layerMask = 1 << 9;
                if (Physics.Raycast(rayOrigin, -transform.up, out hit, weaponLength, layerMask))
                {
                    if (hit.transform.TryGetComponent(out Enemy enemy) && !hasDealtDamage.Contains(hit.transform.gameObject))
                    {
                        enemy.TakeDamage(weaponDamage);
                        print("damage");
                        hasDealtDamage.Add(hit.transform.gameObject);
                    }
                }
            }
        }


    }
    public void StartDealDamage()
    {
        Debug.Log("hiiiiiiii");
        canDealDamage = true;
        hasDealtDamage.Clear();
        //Debug.Log("starts to deal dmg");
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
        //Debug.Log("ending to deal dmg");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves the object you want to interact with
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Call a function on the collided object
            collision.gameObject.TryGetComponent(out Enemy enemy);
            enemy.TakeDamage(weaponDamage);
            print("damage");
        }
    }*/
}