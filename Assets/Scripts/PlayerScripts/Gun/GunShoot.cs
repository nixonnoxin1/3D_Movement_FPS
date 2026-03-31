using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Transform BulletSpawnPoint;
    public GameObject BulletPre;
    public int BulletDamage = 25;

    public GameObject Explosion;
    public Animator Recoil;

    public bool IsReloading = false;
    public float reloadTime;

    public bool Rapid_fire;
    public bool Double_Shot;

    public string fireMode = "Single";
    public float BulletSpeed = 30.0f;

    float DelyFire = 0.0f;

    bool canShoot;
    bool isShooting;

    public int Ammo;
    public int MaxAmmo = 10;


    public Audio_Manager_Script AMS;
    // Start is called before the first frame update
    void Start()
    {
        reloadTime = 0.8f;
        isShooting = false;
        Ammo = MaxAmmo;
        Rapid_fire = false;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        AmmoHandler();
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
        isShooting = true;
        yield return new WaitForSeconds(DelyFire);
        isShooting = false;
        canShoot = true;
    }

    IEnumerator TimeBetweenReload(float reloadTime)
    {
        IsReloading = true;
        yield return new WaitForSeconds(reloadTime);
        Ammo = MaxAmmo;
        canShoot = true;
        IsReloading = false;
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
        if (Input.GetMouseButton(0) && fireMode == "Single" && canShoot == true)
        {
            DelyFire = 0.4f;
            StartCoroutine(TimeBetweenBullets(DelyFire));
            GameObject bullet = Instantiate(BulletPre, BulletSpawnPoint.position, Quaternion.identity); //create bullet
            Ammo--;
            AMS.PlayGunFire(); // play sound
            Recoil.SetTrigger("Recoil");
            Explosion.GetComponent<ParticleSystem>().Play();
            bullet.SetActive(true);// set bullet to true so ytou can see it
            bullet.GetComponent<Rigidbody>().velocity = BulletSpawnPoint.forward * BulletSpeed;//set bullet velocity
            StartCoroutine(Wait(bullet));// time in between bullet so you cant rapidfire
        }
    }

    void RapidFire()
    {
        if (fireMode == "Rapid_fire" && Input.GetMouseButton(0) && canShoot == true)
        {
            DelyFire = 0.1f;
            StartCoroutine(TimeBetweenBullets(DelyFire));
            GameObject bullet = Instantiate(BulletPre, BulletSpawnPoint.position, Quaternion.identity);
            Ammo--;
            AMS.PlayGunFire(); // play sound
            Explosion.GetComponent<ParticleSystem>().Play();// play explosion
            Recoil.SetTrigger("Recoil"); // play recoil animation
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = BulletSpawnPoint.forward * BulletSpeed;
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

            Ammo--;
            Ammo--;

            AMS.PlayGunFire(); // play sound
            Explosion.GetComponent<ParticleSystem>().Play();// play explosion
            Recoil.SetTrigger("Recoil"); // play recoil animation

            bullet1.SetActive(true);
            bullet2.SetActive(true);

            bullet1.GetComponent<Rigidbody>().velocity = BulletSpawnPoint.forward * BulletSpeed;
            bullet2.GetComponent<Rigidbody>().velocity = BulletSpawnPoint.forward * BulletSpeed;

            StartCoroutine(Wait(bullet1));
            StartCoroutine(Wait(bullet2));

        }
    }

    void AmmoHandler()
    {
        if (Ammo <= 0)
        {
            canShoot = false;
        }


        if (Input.GetKeyDown(KeyCode.R) && IsReloading == false && isShooting == false && Ammo < MaxAmmo)
        {
            StartCoroutine(TimeBetweenReload(reloadTime));
        }
    }
}
