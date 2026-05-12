using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManagerScript : MonoBehaviour
{
    public GameObject Player;

    public Image ForeGround;

    public float PlayerHealth = 100;
    public float MaxPlayerHealth;
    // Start is called before the first frame update
    void Start()
    {
        MaxPlayerHealth = PlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void killPlayer(float damage)
    {
        PlayerHealth -= damage;

        ForeGround.fillAmount = PlayerHealth / 100;

        if (PlayerHealth <= 0)
        {
            Destroy(Player.gameObject);
        }
    }

    public void UpdateHeathbar()
    {
        ForeGround.fillAmount = PlayerHealth / 100;
    }

   
}
