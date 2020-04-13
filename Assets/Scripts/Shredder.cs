using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Shredder collided with");
        Destroy(collision.gameObject);

    }

}
