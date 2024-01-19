using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeVarit
{
    alkupisteVari = 0,
    loppupisteVari,
    este,
    Wrong,
    normi,
    openList,
    ClosedList,
    Reitti

}

public class A_StarNode : MonoBehaviour
{ 
    public bool m_este = false;
    public bool alkupiste = false;
    public bool loppupiste = false;

    public Material[] kaikkiVarit;

    public ArrayList naapurit = new ArrayList();

    public A_StarNode[] naapuriNodets;

    public float f = 0;
    public float g = 0;
    public float h = 0;

    public A_StarNode vanhempi;
    
    public void VaihdaVari(NodeVarit variIndeksi)
    {
        //m_este = true;
        MeshRenderer mr =GetComponent<MeshRenderer>();
        mr.sharedMaterial = kaikkiVarit[(int)variIndeksi];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
