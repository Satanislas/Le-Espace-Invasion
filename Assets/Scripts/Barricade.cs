using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Barricade : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    public ParticleSystem particle;
    public GameObject deathBarricadeEffect;
    public float deathEffectTime;
    private SpriteRenderer renderer;

    public AudioSource hitSound;

   

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        renderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit");
        if (!collision.gameObject.CompareTag("bullet") && !collision.gameObject.CompareTag("bulletEnemy")) return; //if not a bullet
        
        //particle handling
        if (particle.isEmitting)
        {
            particle.Stop();
        }
        particle.transform.position = new Vector3(collision.transform.position.x, particle.transform.position.y,particle.transform.position.z);

        //if the bullet comes from allies side
        particle.transform.LookAt(collision.transform); 
      
        particle.Play();
        hitSound.Play();

        Debug.Log("Barricade Hit");
        Destroy(collision.gameObject);
        

        TakeDamage();
    }
    
    
    void TakeDamage()
    {
        currentHealth--;
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, (float)currentHealth / maxHealth);

       
        if (currentHealth <= 0)
        {
            Debug.Log("Barricade destroyed");
            Instantiate(deathBarricadeEffect,transform.position,transform.rotation);
            Destroy(deathBarricadeEffect,deathEffectTime);
            Destroy(gameObject);
        }
    }
}
