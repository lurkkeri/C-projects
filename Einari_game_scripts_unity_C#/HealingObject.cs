using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tätä olisi voinut varmasti periyttää ja overridea, mutta opittiin se paljon myöhemmin kuin tämä oli tehty
public class HealingObject : MonoBehaviour
{
    public int healthIncreaseAmount = 2;
    EinariHealth m_einariHealth;
  
    void Start()
    {
        m_einariHealth = FindFirstObjectByType<EinariHealth>();
    }

    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("Einari"))
        {
            Debug.Log("osuin:" + collision.gameObject.name);
            m_einariHealth.HealPlayer(healthIncreaseAmount);
            Destroy(gameObject);
        }
    }
}
