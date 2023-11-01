using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5;
    private GameObject focalPoint;
    public bool hasPowerup;
    private float powerUpStrength = 15;
    public GameObject powerupIndicator;
    public ParticleSystem orangePowerup;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        orangePowerup.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            powerupIndicator.gameObject.SetActive(true);
            orangePowerup.gameObject.SetActive(true);
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayfromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collided with: " + collision.gameObject.name + "with powerup set to " + hasPowerup);
            enemyRigidbody.AddForce(awayfromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
        orangePowerup.gameObject.SetActive(false);
    }
}
