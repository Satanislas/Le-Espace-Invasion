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
  public ParticleSystem particle;

  public GameObject life1;
  public GameObject life2;
  public GameObject life3;
  private int currentHealth;
  public float invinsibleTimer;
  private float invinsibleTime;
  private GameObject shot;

  public AudioSource ExplosionSound;
  public AudioSource shootingSound;
  private void Start()
  {
    Enemy.OnEnemyDied += EnemyOnEnemyDied;
    currentHealth = 3;
    anim = GetComponent<Animator>();
    rend = GetComponent<Renderer>();
    invinsibleTime = invinsibleTimer;
    shot = null;
  }

  // Update is called once per frame
    void Update()
    {
      if (isDead) return;
      invinsibleTime -= Time.deltaTime;
      
      //shoot
      if (Input.GetKeyDown(KeyCode.Space))
      {
        Shoot();
      }

      float movement = Input.GetAxisRaw("Horizontal");
      if (movement != 0)
      {
        if (!particle.isEmitting)
          particle.Play();
        Move(movement);
      }
      else
      {
        if (particle.isEmitting)
          particle.Stop();
      }
    }

    private void Shoot()
    {
      if (shot != null) return;
      shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
      anim.SetTrigger("Fire");
      shootingSound.Play();
      
      Destroy(shot, 2f);
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
        if (!(invinsibleTime > 0) && !isDead)
          Death();
      }
    }

    void Death()
    {
      isDead = true;
      currentHealth--;
      switch (currentHealth)
      {
        case 2:
          life3.SetActive(false);
          break;
        case 1:
          life2.SetActive(false);
          break;
        case 0: //DEATH BUT THE REAL ONE
          life1.SetActive(false);
          isDead = true;
          anim.SetBool("IsDead",true);
          if (particle.isEmitting)
            particle.Stop();
          SceneFader.sceneFader.LoadScene("Credits");
          return;
      }
      
      StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
      isDead = true;
      anim.SetBool("IsDead",true);
      ExplosionSound.Play();
      if (particle.isEmitting)
        particle.Stop();
      yield return new WaitForSeconds(timeToRespawn);
      anim.SetBool("IsDead",false);
      isDead = false;
      invinsibleTime = invinsibleTimer;
    }
}
