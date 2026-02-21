using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Transform BulletSpawnPoint;
    public GameObject BulletPre;

    public float BulletSpeed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(BulletPre, BulletSpawnPoint.position , Quaternion.identity);
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * BulletSpeed;
            StartCoroutine(Wait(bullet));
            print("shooting");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            FindObjectOfType<PlayerMovement>().CoinCount++;
        }
    }

    IEnumerator Wait(GameObject bullet)
    {
        yield return new WaitForSeconds(1f);
        Destroy(bullet);
    }
}
