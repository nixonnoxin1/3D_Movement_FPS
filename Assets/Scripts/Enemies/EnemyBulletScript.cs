using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public HealthManagerScript HMS;
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
        yield return new WaitForSeconds(1.3f);
        Destroy(this.gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && !collision.collider.GetComponentInChildren<SwordSript>().isBlocking)
        {
            //print("Player Hit!");
            HMS.killPlayer(Damage);
            //print(HMS.PlayerHealth);
        }
    }
}
