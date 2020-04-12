using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    Coroutine firingCoroutine;
    BoxCollider2D myCollider;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float health = 10f;
    float maxHealth = 100f;

   
    // Start is called before the first frame update
    void Start()
    {

        SetUpMoveBoundaries();
        myCollider = FindObjectOfType<BoxCollider2D>();
       
       
    }

  

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        TakeDamage();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
         firingCoroutine = StartCoroutine(FireContinuously());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject laser =
            Instantiate(laserPrefab,
          transform.position,
          Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, projectileSpeed));
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
 
    }

 



    public void TakeDamage()
    {
        float currentHealth = maxHealth;
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Projectile")))
        {
            currentHealth = maxHealth - health;
            Debug.Log("Collision happened" + currentHealth);


        }
       
       
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed ;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
      
        var newXPos = Mathf.Clamp(transform.position.x + deltaX , xMin + padding, xMax - padding);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin + padding, yMax - padding);

        transform.position = new Vector2(newXPos, newYPos);
    
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameGamera = Camera.main;
        xMin = gameGamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameGamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        yMin = gameGamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameGamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }
}
