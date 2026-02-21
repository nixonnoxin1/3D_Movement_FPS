using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            PlayerMovement.FindAnyObjectByType<PlayerMovement>().GetComponent<PlayerMovement>().CoinCount = +1;
            print(PlayerMovement.FindAnyObjectByType<PlayerMovement>().GetComponent<PlayerMovement>().CoinCount);
        }
    }   
}
