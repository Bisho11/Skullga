using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isFighting = animator.GetBool("isFighting");
        bool fight = Input.GetKey(KeyCode.Mouse0);

        if (!isFighting && fight)
        {
            animator.SetBool("isFighting", true);
        }

        if (isFighting && !fight)
        {
            animator.SetBool("isFighting", false);
        }
    }
}
