using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class sienirypp‰ille
public class ClusterHealingObject : MonoBehaviour
{   
    public int healthIncreaseAmount = 5;
    EinariHealth m_einariHealth;
    
    void Start()
    {
        m_einariHealth = FindFirstObjectByType<EinariHealth>();
    }

    void Update()
    {

    }
  
    //private void OnTriggerEnter(Collider other), oma muistiinpano
    //if (other.CompareTag("Player")), oma muistiinpano
    
    // Tuhotaan gameObject ja lis‰t‰‰n Einarille healthia, jos se osuu Einariin
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Einari"))
        {
            Debug.Log("osuin: on collision" + collision.gameObject.name);
            m_einariHealth.HealPlayer(healthIncreaseAmount);
            Destroy(gameObject);
        }
    }
}

