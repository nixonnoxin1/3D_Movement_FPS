using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class UI_Handler : MonoBehaviour
{
    //pause menu 
    bool GamePaused = false;
    public GameObject PauseMenuGameObject;
    public Button unPauseButton;


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
        //PauseMenu();

        EnemiesKilledText.text = "Enemies Killed: " + FindObjectOfType<PlayerController>().EnemiesKilled;
        CoinCountText.text = "Coins: " + FindAnyObjectByType<PlayerController>().CoinCount;
        BulletCounterText.text = FindAnyObjectByType<WeaponManager>().Ammo + "/" + FindAnyObjectByType<WeaponManager>().MaxAmmo;
        TimeCounter.text = "Time: " + Time.deltaTime;
    }

    public void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.M) && GamePaused == false)
        {
            GamePaused = true;
            PauseMenuGameObject.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.M) && GamePaused == true)
        {
            PauseMenuGameObject.SetActive(false);
            GamePaused = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


}
