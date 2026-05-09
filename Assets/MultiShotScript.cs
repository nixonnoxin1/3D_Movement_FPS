using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (FindAnyObjectByType<BowScript>().MultiShot) { Destroy(this.gameObject); return; }
            FindAnyObjectByType<BowScript>().StartCoroutine(FindAnyObjectByType<BowScript>().MultiShotTimer(10));
            Destroy(this.gameObject);
        }
    }
}
