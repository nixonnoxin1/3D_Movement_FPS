using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CoinCounterUI : MonoBehaviour
{
    public TextMeshProUGUI Text;

    
    // Start is called before the first frame update
    void Start()
    {
        
        Text.text = "Coins: ";
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = "Coins: " + FindAnyObjectByType<PlayerMovement>().CoinCount;
    }
}
