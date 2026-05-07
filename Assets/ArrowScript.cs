using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public int damage;

    // Start is called before the first frame update
    void Awake()
    {
        transform.Rotate(90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
