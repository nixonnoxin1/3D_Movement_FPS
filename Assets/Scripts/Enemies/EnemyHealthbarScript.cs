using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbarScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Image EHealthBarSprite;
    public float currentHealth;
    public float maxHealth = 100;

    public GameObject EnemyDeathSorce;
    public Audio_Manager_Script AMS;

    public GameObject HealthbarImage;

    GameObject player;
    GameObject PickupGO;
    List<GameObject> PickUps = new List<GameObject>();

    public float invincibleTime = 0.5f;
    float lastHitTime = -999f;          // start in the past so first hit always lands

    void Awake()
    {
        player = GameObject.FindWithTag("Player");

        EnemyDeathSorce = GameObject.Find("EnemyDeathSoundEffect");
        AMS = FindAnyObjectByType<Audio_Manager_Script>();

        maxHealth = 100;
        currentHealth = maxHealth;

        PickupGO = GameObject.Find("PickUps");

        for (int i = 0; i < PickupGO.transform.childCount; i++)
        {
            PickUps.Add(PickupGO.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        destoryEnemy();
        FacePlayer();
    }


    public void UpdateHealthBar()
    {
         EHealthBarSprite.fillAmount = currentHealth / maxHealth;
    }

    void destoryEnemy()
    {
        if (currentHealth <= 0)
        {
            EnemyDeathSorce.transform.position = this.gameObject.transform.position;
            AMS.PlayEnemyDeath();
            FindObjectOfType<PlayerController>().EnemiesKilled++;
            FindObjectOfType<PlayerController>().CoinCount++;
            Destroy(this.gameObject);

        }
    }

    void LootDrop()
    {
        int SpawnLoot = Random.RandomRange(1, 3);
        print(SpawnLoot);
        Instantiate(PickUps[SpawnLoot], new Vector3(transform.position.x, 1 , transform.position.z), Quaternion.identity);
    }

    void FacePlayer()
    {
        HealthbarImage.transform.rotation = Quaternion.LookRotation(HealthbarImage.transform.position - player.transform.position);
    }

    public void TakeDamage(float amount)
    {
        // Ignore damage if still in invincibility window
        if (Time.time - lastHitTime < invincibleTime) return;

        lastHitTime = Time.time;

        currentHealth -= amount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            LootDrop();
            destoryEnemy();
        }

    }


}
