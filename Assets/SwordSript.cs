using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSript : MonoBehaviour
{

    [Header("Boolans")]
    public bool SwordIsOut;

    public bool isBlocking;
    public bool isAttacking;

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

        if (Input.GetMouseButtonDown(0) && SwordIsOut == true && isAttacking == false)
        {
            Attack();
        }
    }

    public void Attack()
    {
        print("attacking");
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
        if (Input.GetMouseButtonDown(1))
        {
            SwordAnimator.SetTrigger("isBlockingStart");
        }

        if (Input.GetMouseButton(1) && isBlocking == false)
        {
            print("blocking");
            isBlocking = true;
            //play Blocking start animation
            SwordAnimator.SetBool("isBlocking", true);
            //player takes no damage

            //play idle (loop)


        } else if (Input.GetMouseButtonUp(1))
        {
            print("Stoped blocking");
            SwordAnimator.SetBool("isBlocking", false);
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
