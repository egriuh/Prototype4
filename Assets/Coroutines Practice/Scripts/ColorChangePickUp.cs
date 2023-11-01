using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangePickUp : MonoBehaviour
{
    [Header("Materials")]
    public Material defaultMaterial;
    public Material newMaterial;

    [Header("Duration")]
    public float duration;

    //Components

    private MeshRenderer myMr;
    private MeshRenderer playerMr;
    private Collider myCollider;


    // Start is called before the first frame update
    void Start()
    {
        myMr = GetComponent<MeshRenderer>();
        myCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if its a player
        if(other.gameObject.CompareTag("Player"))
        {
            //Get the players mesh renderer
            playerMr = other.gameObject.GetComponent<MeshRenderer>();

            //Start the coroutine
            StartCoroutine(ColorChange());

        }
    }
    IEnumerator ColorChange()
    {
        //Change to new color
        playerMr.material = newMaterial;

        //Wait
        yield return new WaitForSeconds(duration);

        //Change the color back
        playerMr.material = defaultMaterial;
    }
}
