using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator doorAnimator;

    public bool doorState = false;

    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            print("Player inside");
            if (Input.GetKeyDown(KeyCode.E))
            {
                doorState = !doorState; // just toggle it
                doorAnimator.SetTrigger("OpenDoor");
                print("Door state: " + doorState);
            }
        }
    }
}
