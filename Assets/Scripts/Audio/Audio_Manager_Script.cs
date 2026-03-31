using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager_Script : MonoBehaviour
{
    public AudioSource GunSrc;
    public AudioSource CoinSrc;

    public AudioClip GunShot;
    public AudioClip CoinCollect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGunFire()
    {
        //Src is the sorce and you set the audio clip to the audio you want then Play() from the sorce
        GunSrc.clip = GunShot;
        GunSrc.Play();

    }

    public void PlayCoinCollect()
    {
        CoinSrc.clip = CoinCollect;
        CoinSrc.Play();
    }
}
