using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Transform BulletSpawnPoint;
    public GameObject BulletPre;
    public int BulletDamage = 25;


    public bool Rapid_fire;
    public bool Double_Shot;

    public string fireMode = "Single";
    public float BulletSpeed = 30.0f;

    float DelyFire = 0.0f;

    bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        Rapid_fire = false;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleFireModes();
        SingleFire();
        RapidFire();
        DoubleShotFire();
        
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

    IEnumerator TimeBetweenBullets(float DelyFire)
    {
        canShoot = false;
        yield return new WaitForSeconds(DelyFire);
        canShoot = true;
    }

    void ToggleFireModes()
    {
        if (Input.GetKeyDown(KeyCode.Z) && fireMode != "Rapid_fire")
        {
            fireMode = "Rapid_fire";
            print(fireMode);
        }
        else if (fireMode == "Rapid_fire" && Input.GetKeyDown(KeyCode.Z))
        {
            print("Rapid_fire off");
            fireMode = "Single";
        }else if (Input.GetKeyDown(KeyCode.X) && fireMode != "Double_Shot")
        {
            fireMode = "Double_Shot";
        }else if (fireMode == "Double_Shot" && Input.GetKeyDown(KeyCode.Z))
        {
            fireMode = "Single";
        }


    }
    void SingleFire()
    {
        if (Input.GetMouseButtonDown(0) && fireMode == "Single")
        {
            GameObject bullet = Instantiate(BulletPre, BulletSpawnPoint.position, Quaternion.identity);
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * BulletSpeed;
            StartCoroutine(Wait(bullet));
        }
    }

    void RapidFire()
    {
        if (fireMode == "Rapid_fire" && Input.GetMouseButton(0) && canShoot == true)
        {
            DelyFire = 0.1f;
            StartCoroutine(TimeBetweenBullets(DelyFire));
            GameObject bullet = Instantiate(BulletPre, BulletSpawnPoint.position, Quaternion.identity);
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * BulletSpeed;
            StartCoroutine(Wait(bullet));

        }
    }
    
    void DoubleShotFire()
    {
        if (fireMode == "Double_Shot" && Input.GetMouseButton(0) && canShoot == true)
        {
            DelyFire = 0.67f;
            StartCoroutine(TimeBetweenBullets(DelyFire));

            Vector3 potition = BulletSpawnPoint.position;

            GameObject bullet1 = Instantiate(BulletPre, new Vector3(potition.x + 0.5f, potition.y, potition.z) , Quaternion.identity);
            GameObject bullet2 = Instantiate(BulletPre, new Vector3(potition.x + -0.5f, potition.y, potition.z), Quaternion.identity);

            bullet1.SetActive(true);
            bullet2.SetActive(true);

            bullet1.GetComponent<Rigidbody>().velocity = Vector3.forward * BulletSpeed;
            bullet2.GetComponent<Rigidbody>().velocity = Vector3.forward * BulletSpeed;

            StartCoroutine(Wait(bullet1));
            StartCoroutine(Wait(bullet2));

        }
    }
}
