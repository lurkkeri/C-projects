using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIt : MonoBehaviour
{
    GameObject torchLight;

    // Skripti on soihdussa olevassa valossa kiinni.
    void Start()
    {
        torchLight = gameObject;

        if (torchLight == null)
        {
            Debug.Log("ei löydy valoa");
        }

    }
    // Jos pelaaja painaa L, niin valo joko syttyy tai sammuu
    public void ToggleTorch()
    {
        torchLight.SetActive(!torchLight.activeSelf); 
    }
}
