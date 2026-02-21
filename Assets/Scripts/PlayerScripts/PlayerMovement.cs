using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int CoinCount;
    public int EnemiesKilled;

    public float horizontalInput;
    public float verticalInput;

    public GameObject player;

    private Rigidbody rb;
    private Transform PlayerTransform;  

    public int PlayerSpeed;
    public int JumpForce;

    public bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        PlayerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        PlayerTransform.Translate(Vector3.fwd * Time.deltaTime * verticalInput * PlayerSpeed);
        PlayerTransform.Translate(Vector3.right * Time.deltaTime * horizontalInput * PlayerSpeed);

        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

    

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);
            CoinCount++;
        }
    }

}
