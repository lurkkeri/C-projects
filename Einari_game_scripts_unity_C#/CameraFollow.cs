using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset;   


    void Start()
    {
        
    }

    void Update()
    {
        if (target != null)
        {   GameObject player = GameObject.FindWithTag("Player");
            transform.LookAt(player.transform);
            Vector3 loppuPositio = player.transform.position -(player.transform.forward*6f)+(player.transform.up*1f);
            transform.position = Vector3.Lerp(transform.position, loppuPositio, 0.05f);
        }
    }
}
