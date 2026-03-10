using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Handler : MonoBehaviour
{
    public TextMeshProUGUI EnemiesKilledText;
    public TextMeshProUGUI CoinCountText;
    public TextMeshProUGUI TimeCounter;
    public TextMeshProUGUI BulletCounterText;

    // Start is called before the first frame update
    void Start()
    {
        EnemiesKilledText.text = "Enemies Killed: ";
        CoinCountText.text = "Coins: ";
        TimeCounter.text = "Time: ";
    }

    // Update is called once per frame
    void Update()
    {
        EnemiesKilledText.text = "Enemies Killed: " + FindObjectOfType<PlayerController>().EnemiesKilled;
        CoinCountText.text = "Coins: " + FindAnyObjectByType<PlayerController>().CoinCount;
        BulletCounterText.text = FindAnyObjectByType<GunShoot>().Ammo + "/" + FindAnyObjectByType<GunShoot>().MaxAmmo;
        TimeCounter.text = "Time: " + Time.deltaTime;
    }


}
