using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletDamage = 20f;


    // Start is called before the first frame update
    void Awake()
    {
        bulletDamage = 20f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            //Enemy HP-- here insted of destroy
            collision.gameObject.GetComponent<EnemyHealthbarScript>().currentHealth -= bulletDamage;
            collision.gameObject.GetComponent<EnemyHealthbarScript>().UpdateHealthBar();
            //Destroy(collision.collider.gameObject);
        }
    }
}
