using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipementSystem : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    


    
    void Start()
    {
    
    }

   
    public void StartDealDamage()
    {
        weapon.GetComponentInChildren<DamageDealer>().StartDealDamage();
        Debug.Log("Fuck");
    }
    public void EndDealDamage()
    {
        weapon.GetComponentInChildren<DamageDealer>().EndDealDamage();

    }
}