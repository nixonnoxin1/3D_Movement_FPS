using System.Collections;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // ── Weapon Types ─────────────────────────────────────────────────────────
    public enum WeaponType { Pistol, AK, Sword, DoubleShot }

    // ── Inspector References ─────────────────────────────────────────────────
    [Header("Firing")]
    public Transform BulletSpawnPoint;
    public GameObject BulletPrefab;
    public float BulletSpeed = 30f;
    public int BulletDamage = 25;

    [Header("Effects")]
    public GameObject Explosion;
    public Animator Recoil;

    [Header("Weapons")]
    public GameObject AK_Gameobject;
    public GameObject Pistol_Gameobject;   // was GetChild(0) — assign in Inspector
    public GameObject Sword_Gameobject;
    public bool hasAK;

    [Header("Ammo")]
    public int Ammo;
    public int MaxAmmo = 10;
    public bool IsReloading;

    [Header("Audio")]
    public Audio_Manager_Script AMS;

    // ── Cached Components ────────────────────────────────────────────────────
    MeshRenderer _akMesh;
    MeshRenderer _swordMesh;
    BoxCollider _swordCol;
    SwordSript _swordScript;
    ParticleSystem _muzzleFlash;

    // ── Private State ────────────────────────────────────────────────────────
    WeaponType _currentWeapon = WeaponType.Pistol;
    int _pistolAmmo;
    int _akAmmo;
    bool _canShoot;
    bool _isShooting;
    float _reloadTime = 0.8f;

    // ── Unity Lifecycle ──────────────────────────────────────────────────────
    void Awake()
    {
        // Cache components once — no repeated GetComponent calls at runtime
        _akMesh = AK_Gameobject.GetComponent<MeshRenderer>();
        _swordMesh = Sword_Gameobject.GetComponentInChildren<MeshRenderer>();
        _swordCol = Sword_Gameobject.GetComponent<BoxCollider>();
        _swordScript = Sword_Gameobject.GetComponent<SwordSript>();
        _muzzleFlash = Explosion.GetComponent<ParticleSystem>();
    }

    void Start()
    {
        hasAK = false;
        _canShoot = true;
        _isShooting = false;
        _pistolAmmo = 10;
        _akAmmo = 30;
        Ammo = _pistolAmmo;

        EquipWeapon(WeaponType.Pistol);
    }

    void Update()
    {
        HandleWeaponSwitch();
        HandleShooting();
        HandleAmmo();
    }

    // ── Weapon Switching ─────────────────────────────────────────────────────
    void HandleWeaponSwitch()
    {
        if (IsReloading) return;

        if (Input.GetKeyDown(KeyCode.Q))
            CycleGun();

        if (Input.GetKeyDown(KeyCode.Tab))
            EquipWeapon(WeaponType.Sword);
    }

    void CycleGun()
    {
        // Pistol -> AK (if owned) -> Pistol
        if (_currentWeapon == WeaponType.Pistol && hasAK)
            EquipWeapon(WeaponType.AK);
        else
            EquipWeapon(WeaponType.Pistol);
    }

    void EquipWeapon(WeaponType next)
    {
        // Save ammo for the weapon we're leaving
        SaveAmmo();

        _currentWeapon = next;

        // Hide everything first, then show what we need
        HideAllWeapons();

        // Reset shoot ability — the new weapon may have ammo even if the last didn't
        _canShoot = true;

        switch (_currentWeapon)
        {
            case WeaponType.Pistol:
                MaxAmmo = 10;
                Ammo = _pistolAmmo;
                Pistol_Gameobject.SetActive(true);
                break;

            case WeaponType.AK:
                MaxAmmo = 30;
                Ammo = _akAmmo;
                _akMesh.enabled = true;
                break;

            case WeaponType.Sword:
                _swordScript.SwordIsOut = true;
                _swordMesh.enabled = true;
                _swordCol.enabled = true;
                break;
        }
    }

    void HideAllWeapons()
    {
        Pistol_Gameobject.SetActive(false);
        _akMesh.enabled = false;
        _swordScript.SwordIsOut = false;
        _swordMesh.enabled = false;
        _swordCol.enabled = false;
    }

    void SaveAmmo()
    {
        if (_currentWeapon == WeaponType.Pistol) _pistolAmmo = Ammo;
        if (_currentWeapon == WeaponType.AK) _akAmmo = Ammo;
    }

    // ── Shooting ─────────────────────────────────────────────────────────────
    void HandleShooting()
    {
        if (!_canShoot || Ammo <= 0 || !Input.GetMouseButton(0)) return;

        switch (_currentWeapon)
        {
            case WeaponType.Pistol: SingleFire(fireDelay: 0.4f); break;
            case WeaponType.AK: AKFire(fireDelay: 0.1f); break;
            case WeaponType.DoubleShot: DoubleShotFire(fireDelay: 0.67f); break;
        }
    }

    void SingleFire(float fireDelay)
    {
        StartCoroutine(FireCooldown(fireDelay));
        SpawnBullet(BulletSpawnPoint.position);
        Ammo = Mathf.Max(0, Ammo - 1);
        _pistolAmmo = Ammo;
        PlayFireEffects();
    }

    void AKFire(float fireDelay)
    {
        StartCoroutine(FireCooldown(fireDelay));
        SpawnBullet(BulletSpawnPoint.position);
        Ammo = Mathf.Max(0, Ammo - 1);
        _akAmmo = Ammo;
        PlayFireEffects();
    }

    void DoubleShotFire(float fireDelay)
    {
        StartCoroutine(FireCooldown(fireDelay));
        Vector3 pos = BulletSpawnPoint.position;
        SpawnBullet(new Vector3(pos.x + 0.5f, pos.y, pos.z));
        SpawnBullet(new Vector3(pos.x - 0.5f, pos.y, pos.z));
        Ammo = Mathf.Max(0, Ammo - 2);
        PlayFireEffects();
    }

    // Shared bullet spawn — avoids repeating Instantiate + velocity logic
    void SpawnBullet(Vector3 position)
    {
        GameObject bullet = Instantiate(BulletPrefab, position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = BulletSpawnPoint.forward * BulletSpeed;
        StartCoroutine(DestroyAfter(bullet, 1f));
    }

    void PlayFireEffects()
    {
        AMS.PlayGunFire();
        Recoil.SetTrigger("Recoil");
        _muzzleFlash.Play();
    }

    // ── Ammo & Reload ────────────────────────────────────────────────────────
    void HandleAmmo()
    {
        if (Ammo <= 0)
            _canShoot = false;

        bool wantsReload = Input.GetKeyDown(KeyCode.R)
                        && !IsReloading
                        && !_isShooting
                        && Ammo < MaxAmmo;

        if (wantsReload)
            StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        IsReloading = true;
        _canShoot = false;
        yield return new WaitForSeconds(_reloadTime);
        Ammo = MaxAmmo;
        SaveAmmo();
        IsReloading = false;
        _canShoot = true;
    }

    // ── Coroutine Helpers ─────────────────────────────────────────────────────
    IEnumerator FireCooldown(float delay)
    {
        _canShoot = false;
        _isShooting = true;
        yield return new WaitForSeconds(delay);
        _isShooting = false;
        _canShoot = true;
    }

    IEnumerator DestroyAfter(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(obj);
    }

    // ── Trigger (sword kills) ─────────────────────────────────────────────────
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            FindAnyObjectByType<EnemySpawerHandlerScript>().enemies.Remove(other.gameObject);
            Destroy(other.gameObject);
            FindObjectOfType<PlayerMovement>().CoinCount++;
        }
    }
}