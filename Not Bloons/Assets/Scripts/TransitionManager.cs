using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        GameManager.Instance.gameOverEvent += LoseAnimation;
        GameManager.Instance.gameWinEvent += WinAnimation;
    }

    void WinAnimation()
    {
        animator.SetTrigger("Win");
    }
    void LoseAnimation()
    {
        animator.SetTrigger("Lose");
    }
}
