using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poem : MonoBehaviour
{
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Poem collected");
            Destroy(gameObject);
        }
    }
}
