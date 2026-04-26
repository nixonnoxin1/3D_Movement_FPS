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

    [Header("Weapons")]
    public GameObject AK_Gameobject;  
    public GameObject Pistol;
    public GameObject Sword_Gameobject;

    public bool hasAK;


    int AKAmmo;
    int PistolAmmo;
    

    public string GunType = "Pistol";
    public float BulletSpeed = 30.0f;

    float DelyFire = 0.0f;

    bool canShoot;
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
        //RapidFire();
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

    void ToggleFireModes()
    {
        if (Input.GetKeyDown(KeyCode.Z) && GunType != "Rapid_fire")
        {
            GunType = "Rapid_fire";
            print(GunType);
        }
        else if (GunType == "Rapid_fire" && Input.GetKeyDown(KeyCode.Z))
        {
            print("Rapid_fire off");
            GunType = "Single";
        }else if (Input.GetKeyDown(KeyCode.X) && GunType != "Double_Shot")
        {
            GunType = "Double_Shot";
        }else if (GunType == "Double_Shot" && Input.GetKeyDown(KeyCode.Z))
        {
            GunType = "Single";
        }


    }

    void ToggleGunTypes()
    {
        if (Input.GetKeyDown(KeyCode.Q) && GunType == "AK" && IsReloading == false)
        {
            GunType = "Pistol";
            MaxAmmo = 10;
            Ammo = PistolAmmo;

            swordSript.SwordIsOut = false;
            Sword_Gameobject.SetActive(false);

            print("runing1");
            Explosion.GetComponent<Transform>().position += BulletSpawnPoint.forward;

            AK_Gameobject.GetComponent<MeshRenderer>().enabled = false;
            this.transform.GetChild(0).gameObject.SetActive(true);

        }
        else if (hasAK == true && Input.GetKeyDown(KeyCode.Q) && GunType != "AK" && IsReloading == false)
        {
            GunType = "AK";
            MaxAmmo = 30;
            Ammo = AKAmmo;

            swordSript.SwordIsOut = false;
            Sword_Gameobject.SetActive(false);

            print("runing2");

            Explosion.GetComponent<Transform>().position += -BulletSpawnPoint.position;

            AK_Gameobject.GetComponent<MeshRenderer>().enabled = true;
            this.transform.GetChild(0).gameObject.SetActive(false);

        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            GunType = "Sword";

            AK_Gameobject.GetComponent<MeshRenderer>().enabled = false;
            this.transform.GetChild(0).gameObject.SetActive(false);

            swordSript.SwordIsOut = true;
            Sword_Gameobject.SetActive(true);

        }
    }


    void SingleFire()
    {
        if (Input.GetMouseButton(0) && GunType == "Pistol" && canShoot == true)
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

    void RapidFire()
    {
        if (GunType == "Rapid_fire" && Input.GetMouseButton(0) && canShoot == true)
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
            AMS.PlayGunFire(); // play sound
            Explosion.GetComponent<ParticleSystem>().Play();// play explosion
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = BulletSpawnPoint.forward * BulletSpeed;
            StartCoroutine(Wait(bullet));
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
