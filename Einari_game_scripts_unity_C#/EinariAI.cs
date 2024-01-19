using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
//using UnityEditor.Animations;
using UnityEngine;

public class EinariAI : MonoBehaviour
{
    [SerializeField]
    private float m_moveSpeed = 20f;
    [SerializeField]
    private float m_jumpForce = 10f;
    [SerializeField]
    private float m_rotationSpeed = 10f;
    [SerializeField]
    public GameObject ammusPrefab;

    private Rigidbody m_fysiikkaEinari;
    [SerializeField]
    private Animator m_EinariAV;

    private float distanceFromGround;

    public Transform player;

    EinariHealth m_einariHealth;

    public LightIt m_torch;


    void Start()
    {
        m_fysiikkaEinari = GetComponent<Rigidbody>();
        m_EinariAV = GetComponent<Animator>();
        m_einariHealth = GetComponent<EinariHealth>();
        m_torch = FindFirstObjectByType<LightIt>();
        m_torch.ToggleTorch();
    }

    
    void Update()
    {   
        //isGrounded toimii vain CharacterCOntrollerilla? korvasin toiminnon tutkimalla etäisyyttä maasta Raycastilla
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            distanceFromGround = hit.distance;

        }

        if (Input.GetKeyDown("space") && distanceFromGround < 0.2f)
        {
            m_fysiikkaEinari.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            m_EinariAV.SetTrigger("Jump");

        }
        if (Input.GetKeyDown("w") || Input.GetKey("w"))
        {
            m_fysiikkaEinari.AddRelativeForce(Vector3.forward * m_moveSpeed);


        }
        if (Input.GetKeyDown("s") || Input.GetKey("s"))
        {
            m_fysiikkaEinari.AddRelativeForce(Vector3.forward * m_moveSpeed * -1f);

        }
        if (Input.GetKeyDown("a") || Input.GetKey("a"))
        {
            m_fysiikkaEinari.AddRelativeTorque(Vector3.up * m_rotationSpeed * -1f);
        }
        if (Input.GetKeyDown("d") || Input.GetKey("d"))
        {
            m_fysiikkaEinari.AddRelativeTorque(Vector3.up * m_rotationSpeed);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            m_EinariAV.SetTrigger("Shoot");//AmmuAmmus kutsutaan
        }
       
        if (Input.GetKey("l") || Input.GetKey("l"))
        {
            m_EinariAV.SetTrigger("Torch");
            m_torch.ToggleTorch();
        }

        Vector2 einariVelocity = new Vector2(m_fysiikkaEinari.velocity.x, m_fysiikkaEinari.velocity.z);
        float finalVelocity = einariVelocity.magnitude;
        m_EinariAV.SetFloat("Walk", finalVelocity);

    }
    public void AmmuAmmus()
    {
        GameObject piipunSuu = GameObject.Find("Piipunsuu");
        GameObject ammus = Instantiate(ammusPrefab, piipunSuu.transform.position, Quaternion.identity);
        ammus.transform.position = piipunSuu.transform.position;
        Rigidbody lentoRata = ammus.GetComponent<Rigidbody>();
        lentoRata.velocity = Vector3.zero;
        lentoRata.AddForce(piipunSuu.transform.forward * 700f);

    }
    private void OnCollisionEnter(Collision collision)
    {
        //Jos pelaaja osuu haamuun, lentää hän 1f taaksepäin, damage perustuu collisioniin
        if (collision.gameObject.name.Contains("Ghost"))
        {
            m_EinariAV.SetTrigger("Damage");
            m_einariHealth.TakeDamage(2);
            Debug.Log("Aaaarrrgghh!!Sattuuu!!");
            gameObject.transform.position = transform.position + new Vector3(-1f, 0f, 0f);
        }
        // Laava kentästä saa myös damagea
        if (collision.gameObject.name.Contains("Lava"))
        {
            m_einariHealth.TakeDamage(1);
            m_EinariAV.SetTrigger("Damage");
            transform.position = transform.position + new Vector3(0f, 1.5f, 0f);
        }


     }
}

    

