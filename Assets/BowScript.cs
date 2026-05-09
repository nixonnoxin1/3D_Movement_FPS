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
    public float ArrowSpeed;
    
    [Header("Components")]
    public Animator BowAnimator;
    public GameObject Arrow;
    public Transform ArrowSpawn;


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

        if (Input.GetMouseButtonDown(0) && !isAttacking && BowIsOut)
        {

            AttackInputHeld = true;
            BowAnimator.SetTrigger("StartShooting");  
            BowAnimator.SetBool("IsHolding", true); 


        }
        else if (Input.GetMouseButtonUp(0) && !isAttacking && AttackInputHeld)
        {

            AttackInputHeld = false;
            BowAnimator.ResetTrigger("StartShooting");
            BowAnimator.SetBool("IsHolding", false);

            Vector3 shootDirection = Camera.main.transform.forward;
            Quaternion arrowRotation = Quaternion.LookRotation(shootDirection);

            GameObject ArrowObject = Instantiate(Arrow, ArrowSpawn.position, arrowRotation);
            ArrowObject.GetComponent<Rigidbody>().velocity = shootDirection * ArrowSpeed;

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
