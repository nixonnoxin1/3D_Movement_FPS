using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject EnemyDeathSorce;
    public Audio_Manager_Script AMS;
    // Start is called before the first frame update
    void Awake()
    {
        EnemyDeathSorce = GameObject.Find("EnemyDeathSoundEffect");
        AMS = FindAnyObjectByType<Audio_Manager_Script>();
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
            EnemyDeathSorce.transform.position = collision.gameObject.transform.position;
            AMS.PlayEnemyDeath();

            Destroy(collision.collider.gameObject);
            FindObjectOfType<PlayerController>().EnemiesKilled++;
            FindObjectOfType<PlayerController>().CoinCount++;
        }
    }
}
