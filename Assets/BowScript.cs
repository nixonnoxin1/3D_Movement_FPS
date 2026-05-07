using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    [Header("Boolans")]
    public bool BowIsOut;

    public bool isAttacking;
    public bool AttackInputHeld;

    [Header("CoolDowns")]
    public float AttackCooldown;


    [Header("Components")]
    public Animator BowAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Attack()
    {

        if (Input.GetMouseButtonDown(1) && !isAttacking && BowIsOut)
        {

            AttackInputHeld = true;
            BowAnimator.SetTrigger("StartShooting");  // plays StartBlock animation
            BowAnimator.SetBool("IsHolding", true);  // transitions into BlockIdle after StartBlock finishes
            //print("Started blocking");


        }
        else if (Input.GetMouseButtonUp(1))
        {
            AttackInputHeld = false;
            BowAnimator.ResetTrigger("StartShooting"); // clear any leftover trigger
            BowAnimator.SetBool("IsHolding", false);
            StartCoroutine(AttackCooldownTimer(AttackCooldown));
        }

    }

    IEnumerator AttackCooldownTimer(float CooldownTime)
    {
        isAttacking = true;
        // Wait for the specified number of seconds
        yield return new WaitForSeconds(CooldownTime);
        isAttacking = false;

    }
}
