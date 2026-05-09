using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    [Header("Boolans")]
    public bool BowIsOut;
    public bool MultiShot;

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
        MultiShot = false;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (!MultiShot && !isAttacking && BowIsOut)
        {
            if (Input.GetMouseButton(0) && AttackCooldown < 0.2)
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
            else if (Input.GetMouseButtonDown(0) && AttackCooldown > 0.2)
            {

                AttackInputHeld = true;
                BowAnimator.SetTrigger("StartShooting");
                BowAnimator.SetBool("IsHolding", true);


            }
            else if (Input.GetMouseButtonUp(0) && AttackCooldown > 0.2)
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
        multiShot();


    }

    IEnumerator AttackCooldownTimer(float CooldownTime)
    {
        isAttacking = true;
        // Wait for the specified number of seconds
        yield return new WaitForSeconds(CooldownTime);
        isAttacking = false;

    }

    public IEnumerator MultiShotTimer(float Length)
    {
        MultiShot = true;
        yield return new WaitForSeconds(Length);
        MultiShot = false;
    }

    void multiShot()
    {
        if (MultiShot && !isAttacking && BowIsOut)
        {
            if (Input.GetMouseButton(0) && AttackCooldown < 0.2)
            {
                AttackInputHeld = false;
                BowAnimator.ResetTrigger("StartShooting");
                BowAnimator.SetBool("IsHolding", false);

                ArrowShoot(0.5f, 0);
                ArrowShoot(0, 0);
                ArrowShoot(-0.5f, 0);

                StartCoroutine(AttackCooldownTimer(AttackCooldown));
            }
            else if (Input.GetMouseButtonDown(0) && AttackCooldown > 0.2)
            {

                AttackInputHeld = true;
                BowAnimator.SetTrigger("StartShooting");
                BowAnimator.SetBool("IsHolding", true);


            }
            else if (Input.GetMouseButtonUp(0) && AttackCooldown > 0.2 )
            {

                AttackInputHeld = false;
                BowAnimator.ResetTrigger("StartShooting");
                BowAnimator.SetBool("IsHolding", false);

                ArrowShoot(1, 0);
                ArrowShoot(0, 0);
                ArrowShoot(-1, 0);

                StartCoroutine(AttackCooldownTimer(AttackCooldown));
            }
        }
    }

    void ArrowShoot(float offSet, float angle)
    {
        Vector3 shootDirection = Camera.main.transform.forward;
        Quaternion arrowRotation = Quaternion.LookRotation(shootDirection);

        GameObject ArrowObject = Instantiate(Arrow, new Vector3(transform.position.x + offSet, transform.position.y, transform.position.z), arrowRotation);
        ArrowObject.GetComponent<Rigidbody>().velocity = shootDirection * ArrowSpeed;
    }
}
