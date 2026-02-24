using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Handler : MonoBehaviour
{
    public TextMeshProUGUI EnemiesKilledText;
    public TextMeshProUGUI CoinCountText;
    public TextMeshProUGUI TimeCounter;

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
        EnemiesKilledText.text = "Enemies Killed: " + FindObjectOfType<PlayerMovement>().EnemiesKilled;
        CoinCountText.text = "Coins: " + FindAnyObjectByType<PlayerMovement>().CoinCount;
        TimeCounter.text = "Time: " + Time.deltaTime;
    }
}
