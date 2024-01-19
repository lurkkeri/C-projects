using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Einari healthscript
public class EinariHealth : MonoBehaviour
{
    float maxHealth = 10f;
    float health;
    public Slider healthBar;
    public Canvas myCanvas;
    private float m_combatDistance;
    public Transform player;
    bool m_damage;
   
    
    void Start()
    {
        health = maxHealth;
        m_combatDistance = 10f;
        myCanvas = gameObject.GetComponentInChildren<Canvas>();
        healthBar = GetComponentInChildren<Slider>();
        UpdateHealth(health, maxHealth);
        m_damage = false;

    }

    
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Respawn");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            // Check if the current enemy is closer than the previous closest one
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }
     
        float distanceToClosest = Vector3.Distance(closestEnemy.transform.position, transform.position);

        Debug.Log($"Distance: {distanceToClosest}, m_damage: {m_damage}");
        // Jos et‰isyys viholliseen on on pienempi tai yht‰ suuri kuin combat et‰isyys, niin n‰ytet‰‰n healthbar
        if (closestEnemy != null && distanceToClosest < m_combatDistance || m_damage)
        {
            myCanvas.enabled = true;
            Debug.Log("Canvas is enabled - update");
            if (m_damage)
            {
                StartCoroutine("DelayedResetDamage");
            }
   
        }
        else if(distanceToClosest > m_combatDistance && !m_damage )
        {
            StartCoroutine("SetCanvas");
            Debug.Log("Canvas is disabled - update");
        }

    }
    // Yritet‰‰n saada healthbar n‰kym‰‰n oikein
    void LateUpdate()
    {
        Quaternion objectRotation = transform.rotation;
        myCanvas.transform.rotation = objectRotation;
        myCanvas.transform.LookAt(myCanvas.transform.position + Camera.main.transform.rotation * Vector3.forward,
        objectRotation*Vector2.up);
    }
    IEnumerator SetCanvas()
    {
        yield return new WaitForSeconds(3f);
        myCanvas.enabled = false;
    }
    IEnumerator DelayedResetDamage()
    {
        // Wait for a delay before resetting m_damage to false
        yield return new WaitForSeconds(3f);
        m_damage = false;
    }

    public void TakeDamage(int damageAmount)
    {
        m_damage = true;
        health -= damageAmount;

        if (health <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        UpdateHealth(health, maxHealth);
    }
    public void HealPlayer(int healAmount)
    {
        m_damage = true;
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        UpdateHealth(health, maxHealth);

    }
    // N‰ytet‰‰n healthbar aina el‰m‰‰ p‰ivitt‰ess‰
    public void UpdateHealth(float health, float maxHealth)
    {

        healthBar.value = health / maxHealth;
        Debug.Log(healthBar.value);

    }
}
