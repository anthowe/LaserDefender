using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float damage = 10;
   [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float  enemyProjectileSpeed = -0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //EnemyFire();
    }
     public void EnemyFire()
    {
        GameObject laser =
           Instantiate(enemyLaserPrefab,
         transform.position,
         Quaternion.identity) as GameObject;

        laser.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, enemyProjectileSpeed)) * Time.deltaTime;

    }
}
