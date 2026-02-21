using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesKilledScript : MonoBehaviour
{
    public TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        Text.text = "Enemies Killed: ";
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = "Enemies Killed: " + FindObjectOfType<PlayerMovement>().EnemiesKilled;
    }
}
