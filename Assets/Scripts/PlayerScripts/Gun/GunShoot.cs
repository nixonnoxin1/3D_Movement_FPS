using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Transform BulletSpawnPoint;
    public GameObject BulletPre;

    public bool Rapid_fire;
    public float BulletSpeed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        Rapid_fire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Rapid_fire == false)
        {
            GameObject bullet = Instantiate(BulletPre, BulletSpawnPoint.position , Quaternion.identity);
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * BulletSpeed;
            StartCoroutine(Wait(bullet));
        }else if (Input.GetKeyDown(KeyCode.Z) && Rapid_fire != true)
        {
            Rapid_fire = true;
            print("Rapid_fire on");
        }else if (Rapid_fire == true && Input.GetKeyDown(KeyCode.Z))
        {
            print("Rapid_fire off");
            Rapid_fire = false;
        }
        while (Rapid_fire == true && Input.GetMouseButtonDown(0))
        {
            print("shooting");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            FindObjectOfType<PlayerMovement>().CoinCount++;
        }
    }

    IEnumerator Wait(GameObject bullet)
    {
        yield return new WaitForSeconds(1f);
        Destroy(bullet);
    }
}
