using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static WeaponManager;

public class GunShoot : MonoBehaviour
{
    public Transform BulletSpawnPoint;
    public GameObject BulletPre;
    public int BulletDamage = 25;

    public GameObject Explosion;
    public Animator Recoil;

    public bool IsReloading = false;
    public float reloadTime;

    [Header("Weapons")]
    public GameObject AK_Gameobject;  
    public GameObject Pistol;
    public GameObject Sword_Gameobject;
    public GameObject Bow_Gameobject;

    public bool hasAK;


    int AKAmmo;
    int PistolAmmo;


    public enum WeaponType { Pistol, AK, Sword, Bow }
    public string GunType = "Pistol";
    private WeaponType currentWeapon;


    public float BulletSpeed = 30.0f;

    float DelyFire = 0.0f;

    public bool canShoot;
    bool isShooting;

    public int Ammo;
    public int MaxAmmo = 10;


    public Audio_Manager_Script AMS;
    public SwordSript swordSript;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    void Start()
    {
        hasAK = false;
        reloadTime = 0.8f;
        isShooting = false;
        Ammo = MaxAmmo;
        AKAmmo = Ammo;
        PistolAmmo = Ammo;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        AmmoHandler();
        ToggleGunTypes();
        SingleFire();
        DoubleShotFire();
        AK();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            FindAnyObjectByType<EnemySpawerHandlerScript>().enemies.Remove(other.gameObject);
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
        if (GunType == "Pistol")
        {
            PistolAmmo = Ammo;
        }
        if (GunType == "AK")
        {
            AKAmmo = Ammo;
        }
        canShoot = true;
        IsReloading = false;
    }


    void ToggleGunTypes()
    {
        if (IsReloading || isShooting || swordSript.isAttacking || swordSript.isBlocking) return;


        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentWeapon == WeaponType.AK)
                SwitchWeapon(WeaponType.Pistol);
            else if (hasAK)
                SwitchWeapon(WeaponType.AK);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (currentWeapon != WeaponType.Sword)
                SwitchWeapon(WeaponType.Sword);
            else if (currentWeapon == WeaponType.Sword)
                SwitchWeapon(WeaponType.Bow);
        }
    }

    void SwitchWeapon(WeaponType newWeapon)
    {
        // Disable all first
        AK_Gameobject.GetComponent<MeshRenderer>().enabled = false;
        Pistol.SetActive(false);
        Sword_Gameobject.SetActive(false);
        swordSript.SwordIsOut = false;

        currentWeapon = newWeapon;

        switch (newWeapon)
        {
            case WeaponType.Pistol:
                GunType = "Pistol";
                MaxAmmo = 10;
                Ammo = PistolAmmo;
                canShoot = PistolAmmo > 0;  // restore canShoot based on saved ammo
                Pistol.SetActive(true);
                break;

            case WeaponType.AK:
                GunType = "AK";
                MaxAmmo = 30;
                Ammo = AKAmmo;
                canShoot = AKAmmo > 0;      // restore canShoot based on saved ammo
                AK_Gameobject.GetComponent<MeshRenderer>().enabled = true;
                break;

            case WeaponType.Sword:
                GunType = "Sword";
                swordSript.SwordIsOut = true;
                Sword_Gameobject.SetActive(true);
                break;
        }
    }




    void SingleFire() // Pistol
    {
        if (Input.GetMouseButton(0) && GunType == "Pistol" && canShoot)
        {
            DelyFire = 0.4f;
            StartCoroutine(TimeBetweenBullets(DelyFire));
            GameObject bullet = Instantiate(BulletPre, BulletSpawnPoint.position, Quaternion.identity); //create bullet
            Ammo--;
            PistolAmmo = Ammo;
            AMS.PlayGunFire(); // play sound
            Recoil.SetTrigger("Recoil");
            Explosion.GetComponent<ParticleSystem>().Play();
            bullet.SetActive(true);// set bullet to true so ytou can see it
            bullet.GetComponent<Rigidbody>().velocity = BulletSpawnPoint.forward * BulletSpeed;//set bullet velocity
            StartCoroutine(Wait(bullet));// time in between bullet so you cant rapidfire
        }
    }
    
    void DoubleShotFire()
    {
        if (GunType == "Double_Shot" && Input.GetMouseButton(0) && canShoot == true)
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

    void AK()
    {
        if (GunType == "AK" && Input.GetMouseButton(0) && canShoot == true)
        {
            DelyFire = 0.1f;
            StartCoroutine(TimeBetweenBullets(DelyFire));
            GameObject bullet = Instantiate(BulletPre, BulletSpawnPoint.position, Quaternion.identity);
            Ammo--;
            AKAmmo = Ammo;
            //canShoot = AKAmmo > 0;      // restore canShoot based on saved ammo
            AMS.PlayGunFire(); // play sound
            Explosion.GetComponent<ParticleSystem>().Play();// play explosion
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = BulletSpawnPoint.forward * BulletSpeed;
            StartCoroutine(Wait(bullet));
        }
    }


    void AmmoHandler()
    {
        if (AKAmmo <= 0 && currentWeapon == WeaponType.AK) canShoot = false;
        else if (PistolAmmo <= 0 && currentWeapon == WeaponType.Pistol) canShoot = false;


        if (Input.GetKeyDown(KeyCode.R) && !IsReloading && !isShooting && Ammo < MaxAmmo)
        {
            StartCoroutine(TimeBetweenReload(reloadTime));

        }

    }
}
