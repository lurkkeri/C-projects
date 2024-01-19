using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class A_star : MonoBehaviour
{
    public GameObject m_node;

    [MenuItem("A_star/Generoiruudukko")]

    static void Generoiruudukko()
    {
        Debug.Log("Generoi ruudukko");
        for (int x = 0; x < 10; x++)
        {
            for (int i = 0; i < 10; i++)
            {
                //Muista aina kästäys, sillä voi päästä pinteestä
                GameObject m_node = (GameObject)Resources.Load("Node");
                GameObject klooniNode = Instantiate(m_node);
                klooniNode.transform.name = m_node.name + i + "_" + x;
                klooniNode.transform.position = new Vector3(0f + x, 0f, 0f + i);
            }
        }
    }
    [MenuItem("A_star/Poistaruudukko")]
    static void Poistaruudukko()
    {
        GameObject[] kaikkiNodet = GameObject.FindGameObjectsWithTag("AstraNode");
        for (int i = 0; i < kaikkiNodet.Length; i++)
        {
            GameObject teuras = kaikkiNodet[i];
            DestroyImmediate(teuras);
        }
    }
    [MenuItem("A_star/TarkastaEsteet")]
    static void TarkastaEsteet()
    {

        GameObject[] kaikkiNodet = GameObject.FindGameObjectsWithTag("AstraNode");
        for (int i = 0; i < kaikkiNodet.Length; i++)
        {
            GameObject este = kaikkiNodet[i];
            A_StarNode a_StarNode = este.GetComponent<A_StarNode>();
            RaycastHit hit = new RaycastHit();

            //lähtöpaikka, suunta, varastoitu tieto osumasta
            if (Physics.Raycast(este.transform.position, Vector3.up, out hit))
            {
                Debug.Log("osuttiin: " + hit.collider.name);

                a_StarNode.VaihdaVari(NodeVarit.Wrong);
                a_StarNode.m_este = true;
            }

            if (a_StarNode.alkupiste)
            {
                Debug.Log("alkupiste löydetty" + a_StarNode.name);
                a_StarNode.VaihdaVari(NodeVarit.alkupisteVari);
            }
            if (a_StarNode.loppupiste)
            {
                Debug.Log("loppupiste löydetty" + a_StarNode.name);
                a_StarNode.VaihdaVari(NodeVarit.loppupisteVari);
            }
        }
    }
    [MenuItem("A_star/EtsiNaapurit")]
    static void EtsiNaapurit()
    {

        GameObject[] kaikkiNodet = GameObject.FindGameObjectsWithTag("AstraNode");
        for (int i = 0; i < kaikkiNodet.Length; i++)
        {
            GameObject missaNaapuri = kaikkiNodet[i];
            A_StarNode a_StarNode = missaNaapuri.GetComponent<A_StarNode>();
            for(int x = 0; x < kaikkiNodet.Length; x++) 
            {
                GameObject ehkaNaapuri = kaikkiNodet[x];
                A_StarNode etsitaanNaapuri = ehkaNaapuri.GetComponent<A_StarNode>();
                if (ehkaNaapuri.name != missaNaapuri.name)
                {
                    float etaisyys = Vector3.Distance(missaNaapuri.transform.position, ehkaNaapuri.transform.position);

                    if (etaisyys < 1.8f)
                    {
                        a_StarNode.naapurit.Add(etsitaanNaapuri);
                    }
                }
                a_StarNode.naapuriNodets = new A_StarNode[a_StarNode.naapurit.Count];
                a_StarNode.naapurit.CopyTo(a_StarNode.naapuriNodets);
            }
        }
    }
}
