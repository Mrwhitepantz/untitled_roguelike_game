using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public GameObject gameover;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            DamageToPlayer(10);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            HealthToPlayer(10);
        }*/
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Square")
        {
            DamageToPlayer(20);
        }
        if (other.gameObject.name == "Charmander")
        {
            DamageToPlayer(25);
        }
        else if (other.gameObject.name == "testBullet 1(Clone)")
        {
            DamageToPlayer(5);
        }
        else if (other.gameObject.name == "testBullet 2(Clone)")
        {
            DamageToPlayer(10);
        }
        else if (other.gameObject.name == "HealthPot")
        {
            HealthToPlayer(20);
            Destroy(other.gameObject);
        }
        
    }
    public void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.name.ToString().Contains("PotionRed")){
            HealthToPlayer(20);
            
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.name == "Leek"){
            DamageToPlayer(100);
            
            Destroy(collision.gameObject);
        }

    }


    void DamageToPlayer(int damage)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            healthBar.SetHealth(currentHealth);
        }
        else
        {
            currentHealth= 0;

            healthBar.SetHealth(currentHealth);
            Time.timeScale = 0;
            gameover.SetActive(true);
        }
    }

    void HealthToPlayer(int heal)
    {
        currentHealth += heal;

        if (currentHealth < maxHealth)
        {
            healthBar.SetHealth(currentHealth);
        }

        else
        {
            currentHealth = maxHealth;

            healthBar.SetHealth(currentHealth);
        }
    }
}
