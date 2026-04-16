using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbarScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Image EHealthBarSprite;
    public float currentHealth;
    public float maxHealth;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void UpdateHealthBar()
    {
         EHealthBarSprite.fillAmount = currentHealth / maxHealth;
    }
}
