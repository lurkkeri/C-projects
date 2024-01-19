using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{   
    // Enemy healthscript
    float maxHealth = 10f;
    float health;
    public Slider healthSlider;
    public Canvas canvas;
    private float m_combatDistance;
    public Transform player;
    public Transform enemy;

    void Start()
    {
        health = maxHealth;
        m_combatDistance = 10f;
        canvas = gameObject.GetComponentInChildren<Canvas>();
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.value = 1f;
        UpdateHealth(health, maxHealth);

    }

    void Update()
    {   
        // Jos pelaajaa ei en�� l�ydy, peli on p��ttynyt
        GameObject player = GameObject.FindWithTag("Player");

        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
       
        // Einari tulee tarpeeksi l�helle, niin helathbar n�kyy
        if (distance <= m_combatDistance)
        {
           canvas.enabled = true;
 
        }
        else 
        {
            canvas.enabled = false;
           
        }

    }
    // Yritet��n saada healthbar n�kym��n oikein
    void LateUpdate()
    {
        canvas.transform.LookAt(canvas.transform.position + Camera.main.transform.rotation * Vector3.forward,
        Camera.main.transform.rotation * Vector3.up);
    }
    // Einarin damage metodi
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount; 

        if (health <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        UpdateHealth(health, maxHealth);
    }
    // p�ivitet��n health n�kym��n healthbarissa
    public void UpdateHealth(float health, float maxHealth)
    {
        healthSlider.value = health/maxHealth;
        Debug.Log(healthSlider.value);
        

    }
}
