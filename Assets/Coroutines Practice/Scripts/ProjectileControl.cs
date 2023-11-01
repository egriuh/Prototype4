using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour
{
    public float moveSpeed;
    public float destroyDelay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move Forward
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Destroyable"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
