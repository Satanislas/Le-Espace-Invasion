using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
  public GameObject bullet;
  private Animator anim;
  public Transform shottingOffset;
  public float MovementBounds;
  public float speed;
  public float timeToRespawn;
  private bool isDead;
  private Renderer rend;

  private void Start()
  {
    Enemy.OnEnemyDied += EnemyOnEnemyDied;
    anim = GetComponent<Animator>();
    rend = GetComponent<Renderer>();
  }

  // Update is called once per frame
    void Update()
    {
      if (isDead) return;
      
      //shoot
      if (Input.GetKeyDown(KeyCode.Space))
      {
        Shoot();
      }

      float movement = Input.GetAxisRaw("Horizontal");
      if (movement != 0)
      {
        Move(movement);
      }
    }

    private void Shoot()
    {
      GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
      anim.SetTrigger("Fire");
      
      Destroy(shot, 3f);
    }

    private void Move(float movement)
    {
      if (movement > 0 && transform.position.x >= MovementBounds) return;
      if (movement < 0 && transform.position.x <= -MovementBounds) return;
      transform.position += new Vector3(movement * speed * Time.deltaTime, 0,0);
    }
    
    
    void EnemyOnEnemyDied()
    {
    }
    private void OnDestroy()
    {
      Enemy.OnEnemyDied -= EnemyOnEnemyDied;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      
      if (other.gameObject.CompareTag("bulletEnemy"))
      {
        Destroy(other.gameObject);
        Death();
      }
    }

    void Death()
    {
      StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
      rend.enabled = false;
      isDead = true;
      yield return new WaitForSeconds(timeToRespawn);
      rend.enabled = true;
      isDead = false;
    }
}
