using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    Monkey monkey;

    void Start()
    {
        animator = GetComponent<Animator>();
        monkey = GetComponent<Monkey>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = monkey.inputDirection != Vector2.zero;
        animator.SetBool("Walking", isWalking);

        animator.SetFloat("Horizontal", monkey.inputDirection.x);
        animator.SetFloat("Vertical", monkey.inputDirection.y);
    }
}
