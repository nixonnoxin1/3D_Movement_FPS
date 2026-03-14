using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawerHandlerScript : MonoBehaviour
{
    public GameObject EnemyPre;
    public int X;
    public int Y;
    public int Z;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnLocation();
            var Enemy = Instantiate(EnemyPre, new Vector3(X, 1, Z), Quaternion.identity);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnLocation()
    {
        //pick number between 1-5
        int XChosenValue = Random.Range(-22, 17);
        //set SpawnScale to that number
        X = XChosenValue;
        int ZChosenValue = Random.Range(-17, 24);
        Z = ZChosenValue;

    }
}
