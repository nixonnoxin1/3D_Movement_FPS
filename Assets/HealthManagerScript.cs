using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManagerScript : MonoBehaviour
{
    public GameObject Player;

    public Image ForeGround;

    public float PlayerHelth = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ForeGround.fillAmount = PlayerHelth / 100;
    }


    public void killPlayer()
    {
        if (PlayerHelth <= 0)
        {
            Destroy(Player);
        }
    }
}
