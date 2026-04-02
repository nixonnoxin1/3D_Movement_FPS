using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawerHandlerScript : MonoBehaviour
{
    public int NumberOfEnemies;

    public GameObject EnemyPre;
    public List<GameObject> enemies = new List<GameObject>();

    public int[] XRange;
    public int[] YRange;

    int X;
    int Y;
    int Z;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < NumberOfEnemies; i++)
        {
            SpawnLocation();
            var Enemy = Instantiate(EnemyPre, new Vector3(X, 1, Z), Quaternion.identity);
            enemies.Add(Enemy);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        NextSence();
    }

    private void SpawnLocation()
    {
        //pick number between XRange
        int XChosenValue = Random.Range(-22, 17);
        //set SpawnScale to that number
        X = XChosenValue;
        int ZChosenValue = Random.Range(-17, 24);
        Z = ZChosenValue;

    }

    public void NextSence()
    {
        enemies.RemoveAll(e => e == null);
        if (enemies.Count == 0)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        
    }
}
