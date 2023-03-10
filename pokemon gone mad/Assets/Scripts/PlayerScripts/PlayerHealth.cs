using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DamageToPlayer(10);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            HealthToPlayer(10);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Square")
        {
            DamageToPlayer(20);
        }
        else if (other.gameObject.name == "testBullet 1(Clone)")
        {
            DamageToPlayer(5);
        }
        else if (other.gameObject.name == "HealthPot")
        {
            HealthToPlayer(20);
            Destroy(other.gameObject);
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
