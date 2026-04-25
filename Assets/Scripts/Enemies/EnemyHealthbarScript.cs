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

    void Awake()
    {
        player = GameObject.FindWithTag("Player");

        EnemyDeathSorce = GameObject.Find("EnemyDeathSoundEffect");
        AMS = FindAnyObjectByType<Audio_Manager_Script>();

        maxHealth = 100;
        currentHealth = maxHealth;
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

    void FacePlayer()
    {
        HealthbarImage.transform.rotation = Quaternion.LookRotation(HealthbarImage.transform.position - player.transform.position);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            destoryEnemy();
        }

    }


}
