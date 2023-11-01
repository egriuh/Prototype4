using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleControl : MonoBehaviour
{
    //VARIABLES

    [Header("Movement")]
    public float moveSpeed;
    public float turnSpeed;
    public float jumpForce;
    public bool isOnGround = true;
    private float verticalInput;
    private float horizontalInput;
    private Rigidbody rb;

    [Header("Shooting")]
    public GameObject projectile;
    public float shootDelay;
    public GameObject spawnPoint;
    public bool canShoot = true;


    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //Forward and Backward Movement
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * verticalInput);


        //Clockwise and counterclockwise Rotation
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

        
        //Jumping
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            isOnGround = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }


        
        //Shooting
        if(Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
        {
            StartCoroutine(Shoot());
        }

    }

    IEnumerator Shoot()
    {
        canShoot = false;
        Instantiate(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation);

        //Wait
        yield return new WaitForSeconds(shootDelay);

        canShoot = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
