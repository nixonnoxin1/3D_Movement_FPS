using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    HealthManagerScript HMS;
    public float Damage = 5;
    // Start is called before the first frame update
    void Awake()
    {
        HMS = FindAnyObjectByType<HealthManagerScript>();
        StartCoroutine(DestoryBullet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestoryBullet()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //print("Player Hit!");
            HMS.PlayerHealth -= Damage;
            //print(HMS.PlayerHealth);
        }
    }
}
