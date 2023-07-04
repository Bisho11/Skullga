using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_script : MonoBehaviour
{
    public GameObject player;
    public Image currentWeapon;
    public Sprite hand, sythe;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon.sprite = hand;
    }

    // Update is called once per frame
    void Update()
    {
        Character character = player.GetComponent<Character>();

        if (character.movementSM.currentState.GetType() == typeof(CombatState) || character.movementSM.currentState.GetType() == typeof(AttackState))
        {

            currentWeapon.sprite = sythe;
        }
        else 
        currentWeapon.sprite = hand;
     
}
}
