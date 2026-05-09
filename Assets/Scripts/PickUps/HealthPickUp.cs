using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public HealthManagerScript HMS;
    // Start is called before the first frame update
    void Start()
    {
        HMS = FindAnyObjectByType<HealthManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (HMS.PlayerHealth < 80) { HMS.PlayerHealth += 20; }
            else if (HMS.PlayerHealth > 80) { HMS.PlayerHealth = HMS.MaxPlayerHealth;  }
            Destroy(this.gameObject);
        }
    }
}
