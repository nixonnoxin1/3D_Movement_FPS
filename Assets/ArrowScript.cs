using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public int ArrowDamage;

    // Start is called before the first frame update
    void Awake()
    {
        transform.Rotate(90f, 0f, 0f);
        StartCoroutine(killArrow(5));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator killArrow(int ArrowFlightTime)
    {

        yield return new WaitForSeconds(ArrowFlightTime);

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //Enemy HP-- here insted of destroy
            collision.gameObject.GetComponent<EnemyHealthbarScript>().TakeDamage(ArrowDamage);
            Destroy(gameObject);
            //Destroy(collision.collider.gameObject);
        }
        else
        {
            StartCoroutine(killArrow(1));
        }
    }
}
