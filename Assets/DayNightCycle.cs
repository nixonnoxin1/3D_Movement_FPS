using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float rate = 1.0f;
     
    public Transform LightTransform;
    public float SunRotation;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeChanger(rate, LightTransform));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

IEnumerator TimeChanger(float RotationRate, Transform LightTransform)
{
    while (true)
    {
        LightTransform.Rotate(RotationRate * Time.deltaTime, 0f, 0f);
        yield return null; // waits one frame
    }
}
}
