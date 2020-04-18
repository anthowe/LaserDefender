﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;


    [Header("Audio Clips")]
    [SerializeField] AudioClip deathClip;
    [SerializeField] [Range(0, 1)] float deathVolume = 1f;
    [SerializeField] AudioClip fireClip;
    [SerializeField] [Range(0,1)] float fireVolume = 1f;

    Coroutine firingCoroutine;
    BoxCollider2D myCollider;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
   

   
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
            AudioSource.PlayClipAtPoint(fireClip, Camera.main.transform.position, fireVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
            
        }
 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }



    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
       
       
    }
    private void Die()
    {
        FindObjectOfType<LoadLevel>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position, deathVolume);
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
