using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDied();

    private Animator anim;

    public static event EnemyDied OnEnemyDied;
    public int scoreValue;
    public GameObject bulletPrefab;
    public float shootTime;
    private float shootTimer;
    public float shootProbability;
    public Transform shottingOffset;

    public AudioSource shootSound;
    public AudioSource deathSound;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
        shootTimer = shootTime;
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            Shoot();
        } 
    }

    void Shoot()
    {
        
        if ((int)Random.Range(1,1/shootProbability) == 1)
        {
            anim.SetTrigger("Fire");
            shootSound.Play();
            GameObject shot = Instantiate(bulletPrefab, shottingOffset.position, Quaternion.identity);
            Destroy(shot, 3f);
        }
        shootTimer = shootTime;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.CompareTag("bulletEnemy")) return;
      Destroy(collision.gameObject);
      OnEnemyDied.Invoke();

      Death();
    }

    void Death()
    {
        anim.SetTrigger("Death");
        deathSound.Play();
        
        //calls because I don't get how event works (and it's resource-consuming to change habits)
        EnemySpawner.enemies.Remove(gameObject);
        ScoreManager.scoreManager.addScore(scoreValue);
    }

    void KillMeCall()
    {
        Destroy(gameObject);
    }
}
