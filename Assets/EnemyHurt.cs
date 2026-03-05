using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurt : MonoBehaviour
{
    private HealthManagerScript HMS;

    // Start is called before the first frame update
    void Start()
    {
        HMS = FindAnyObjectByType<HealthManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            HMS.PlayerHelth -= 20;
        }
    }
}
