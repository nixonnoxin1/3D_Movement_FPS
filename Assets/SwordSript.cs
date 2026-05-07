using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSript : MonoBehaviour
{

    [Header("Boolans")]
    public bool SwordIsOut;

    public bool isBlocking;
    public bool isAttacking;
    public bool blockInputHeld;

    [Header("CoolDowns")]
    public float AttackCooldown;
    public float BlockCooldown;

    [Header("Components")]
    public Animator SwordAnimator;

    public float SwordDamage;
    // Start is called before the first frame update
    void Start()
    {
        SwordDamage = 50;
    }

    // Update is called once per frame
    void Update()
    {
        Blocking();

        if (Input.GetMouseButtonDown(0) && SwordIsOut && !isAttacking)
        {
            SingleAttack();
        }
        if (Input.GetMouseButton(0) && SwordIsOut && !isAttacking)
        {
            TwoSwingAttack();
        }


    }
    public void TwoSwingAttack()
    {
        if (isAttacking) return; // prevent spamming
        print("Two swing attack");
        GetComponent<BoxCollider>().enabled = true;
        SwordAnimator.SetTrigger("TwoSwings");
        StartCoroutine(AttackCooldownTimer(AttackCooldown + (AttackCooldown / 1.5f)));
    }

    public void SingleAttack()
    {
        GetComponent<BoxCollider>().enabled = true;
        SwordAnimator.SetTrigger("SwordAttack");
        StartCoroutine(AttackCooldownTimer(AttackCooldown));
    }

    IEnumerator AttackCooldownTimer(float CooldownTime)
    {
        isAttacking = true;
        // Wait for the specified number of seconds
        yield return new WaitForSeconds(CooldownTime);
        isAttacking = false;

    }

    public void Blocking()
    {

        if (Input.GetMouseButtonDown(1) && !isBlocking && !isAttacking)
        {

            blockInputHeld = true;
            isBlocking = true;
            SwordAnimator.SetTrigger("StartBlocking");  // plays StartBlock animation
            SwordAnimator.SetBool("isBlocking", true);  // transitions into BlockIdle after StartBlock finishes
            print("Started blocking");


        } else if (Input.GetMouseButtonUp(1))
        {
            blockInputHeld = false;
            SwordAnimator.SetBool("isBlocking", false); // triggers EndBlock → Idle transition
            print("Stopped blocking");
            StartCoroutine(BlockCooldownTimer(BlockCooldown));
        }

    }

    IEnumerator BlockCooldownTimer(float CooldownTime)
    {
        // Wait for the specified number of seconds
        yield return new WaitForSeconds(CooldownTime);

        isBlocking = false;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isAttacking)
        {
            other.GetComponent<EnemyHealthbarScript>().TakeDamage(SwordDamage);
        }
    }
}
