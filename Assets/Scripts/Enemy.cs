using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100f;
    [SerializeField] int scoreValue = 150;

    [Header("Enemy Combat")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = .2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] GameObject enemyExplosionPrefab;
    [SerializeField] float enemyProjectileSpeed = 10f;
    [SerializeField] float durationOfExplosion = 5f;

    [Header("Audio Clips")]
    [SerializeField] AudioClip deathClip;
    [SerializeField] [Range(0, 1)] float deathVolume = 1f;
    [SerializeField] AudioClip fireClip;
    [SerializeField] [Range(0, 1)] float fireVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject enemyLaser = Instantiate
            (enemyLaserPrefab, 
            transform.position, 
            Quaternion.identity) as GameObject;
        enemyLaser.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, -enemyProjectileSpeed));
        AudioSource.PlayClipAtPoint(fireClip, Camera.main.transform.position, fireVolume);
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
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosionFX = Instantiate(
            enemyExplosionPrefab,
            transform.position,
           transform.rotation);
        AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position, deathVolume);
        Destroy(explosionFX, durationOfExplosion);
    }
   
   
}
