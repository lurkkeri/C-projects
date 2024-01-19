using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// T�t� olisi voinut varmasti periytt�� ja overridea, mutta opittiin se paljon my�hemmin kuin t�m� oli tehty
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
