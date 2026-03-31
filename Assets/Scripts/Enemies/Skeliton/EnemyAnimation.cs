using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Idle
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isRunning", false);
        }

        // Run
        if (Input.GetKeyDown(KeyCode.O))
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
        }

        // Attack
        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetTrigger("attack");
        }
    }
}