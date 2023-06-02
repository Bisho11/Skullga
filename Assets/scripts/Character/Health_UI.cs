using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_UI : MonoBehaviour
{
    public GameObject player;
    public Image heart1,heart2,heart3;
    public Sprite _default, _dmg;

    // Update is called once per frame
    private void Start()
    {
        heart1.sprite = _default;
        heart2.sprite = _default;
        heart3.sprite = _default;
    }



    void Update()
    {
 
        if (player.GetComponent<HealthSystem>().health == 2)
        {
            heart1.sprite = _default;
            heart2.sprite = _default;
            heart3.sprite = _dmg;
        }
        else if (player.GetComponent<HealthSystem>().health == 1)
        {
            heart1.sprite = _default;
            heart2.sprite = _dmg;
            heart3.sprite = _dmg;
        }
        if (player.GetComponent<HealthSystem>().health <= 0)
        {
            heart1.sprite = _dmg;
            heart2.sprite = _dmg;
            heart3.sprite = _dmg;
        }



    }
}
