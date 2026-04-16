using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject connectedTeleporter;

    public bool onCooldown = false;
    public float cooldownTime;

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
        if (other.CompareTag("Player") && onCooldown == false)
        {
            other.GetComponent<CharacterController>().enabled = false;
            connectedTeleporter.GetComponent<Teleporter>().onCooldown = true;
            other.transform.position = connectedTeleporter.transform.position;

            StartCoroutine(Cooldown(cooldownTime));
            other.GetComponent<CharacterController>().enabled = true;
            print(connectedTeleporter.transform.position);

            

        }
    }

    IEnumerator Cooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        connectedTeleporter.GetComponent<Teleporter>().onCooldown = false;
    }
}
