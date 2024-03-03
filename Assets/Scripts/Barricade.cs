using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Barricade : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    private SpriteRenderer renderer;
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
            Destroy(gameObject);
        }
    }
}
