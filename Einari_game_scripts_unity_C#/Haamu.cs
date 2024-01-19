using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.Animations;

public class Haamu : MonoBehaviour
{
    //public Rigidbody m_fysiikkaHaamu;
    [SerializeField]
    private Animator m_HaamunAV;
    public NavMeshAgent m_agentti;
    HealthScript m_healthScript;

    [SerializeField]
    private float maxHealth = 10f;
    [SerializeField]
    private float health;

    [SerializeField]
    public float range; 
    [SerializeField]
    public Transform centrePoint;

    int counter;

    void Start()
    {
        health = maxHealth;
        //m_fysiikkaHaamu = GetComponent<Rigidbody>();
        m_HaamunAV = GetComponent<Animator>();
        m_agentti = this.GetComponent<NavMeshAgent>();
        m_healthScript = GetComponent<HealthScript>();
        centrePoint = GetComponent<Transform>();
        m_agentti.enabled = true;
    }

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.Log("Game over!");
        }
        // Lasketaan et‰isyydet haamusta pelaajaan ja haamusta sen m‰‰r‰n p‰‰h‰n
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        float distanceFromDest = Vector3.Distance(m_agentti.destination, gameObject.transform.position);

        // Jos et‰isyys on pienempi tai yht‰suuri kuin 10, aletaan jahdata pelaajaa
        if (distance <= 10f)
        {
            m_HaamunAV.SetBool("Jahti", true);
            Debug.Log("T‰‰lt‰ tullaan Einari!");
            m_agentti.SetDestination(player.transform.position);
            Debug.Log($"Destination: {m_agentti.destination}, Position: {transform.position}");

            // Jos et‰isyys on pienempi kuin 1.5 n‰ytet‰‰n hyˆkk‰ysanimaatiota
            if (distance < 1.5f)
            {
                m_HaamunAV.SetTrigger("Attack");
            }
        }
        //Jos haamulla ei ole mit‰‰n kesken tai se on jumissa, annetaan sille uusi random m‰‰r‰np‰‰
        else if ( m_agentti.remainingDistance < 0.1f && !m_agentti.hasPath && distance > 10f )
        {
            m_HaamunAV.SetBool("Jahti", false);
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                m_agentti.SetDestination(point);
                Debug.Log("Uusim‰‰r‰np‰‰ asetettu");
            
            }
        
    
        }
        else if (distance > 10f)
        {
            m_HaamunAV.SetBool("Jahti", false);

        }
       

    }
    private void OnCollisionEnter(Collision collision)
    {
        float distance = Vector3.Distance(m_agentti.destination, gameObject.transform.position);
        
        if (collision.gameObject.name.Contains("Ammus"))
        {
            m_HaamunAV.SetTrigger("Damage");
            m_healthScript.TakeDamage(2);
            Debug.Log("Osui");
        }
        // Lasketaan tˆrm‰ykset, jos kyseess‰ ei ole maa tai pelaaja
        else if(!collision.gameObject.name.Contains("Terrain") || !collision.gameObject.name.Contains("Player"))
        {
            if (counter > 3)
            {
                counter = 0;
            }
            counter++;

        }
 
    }
  
    // Lasketaan random piste haamun m‰‰r‰np‰‰ksi ampumalla Navmesh hit haamusta
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
}
