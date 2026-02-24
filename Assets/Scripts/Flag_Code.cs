using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag_Code : MonoBehaviour
{
    public GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            NextLevel();
        }
    }

    void NextLevel()
    {
        SceneManager.LoadScene("Level2");
    }
}
