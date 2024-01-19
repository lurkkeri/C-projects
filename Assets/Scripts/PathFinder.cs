using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    ArrayList openList = new ArrayList();
    ArrayList closeList = new ArrayList();

    public A_StarNode[] openListVisible;
    public A_StarNode[] closeListVisible;

    public A_StarNode aloitusNode;
    public A_StarNode lopetusNode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            Debug.Log("polunetsintä aloitettu");
            StartCoroutine("PathFinderStart");
        }
    }
    IEnumerator PathFinderStart()
    {
        GameObject[] kaikkiNodet = GameObject.FindGameObjectsWithTag("AstraNode");
        for (int i = 0; i < kaikkiNodet.Length; i++)
        {
            GameObject este = kaikkiNodet[i];
            A_StarNode a_StarNode = este.GetComponent<A_StarNode>();
            if (a_StarNode.alkupiste == true)
            {
                aloitusNode = a_StarNode;
            }
            if (a_StarNode.loppupiste == true)
            {
                lopetusNode = a_StarNode;
            }
        }
        A_StarNode kasiteltavaNode = aloitusNode;
        openList.Add(kasiteltavaNode);
        kasiteltavaNode.VaihdaVari(NodeVarit.openList);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 200; i++) 
        {
            openList.Remove(kasiteltavaNode);
            closeList.Add(kasiteltavaNode);
            //ei muka viittaa mihiknään
            kasiteltavaNode.VaihdaVari(NodeVarit.ClosedList);

            //voi olla tässäkin virhe
            openListVisible = new A_StarNode[openList.Count];
            openList.CopyTo(openListVisible);

            closeListVisible = new A_StarNode[closeList.Count];
            closeList.CopyTo(closeListVisible);

            KasitellaanNode(kasiteltavaNode, kasiteltavaNode.naapuriNodets);

            A_StarNode pieninFarvo = null;
           
            float pieninLoydettyFarvo = float.MaxValue;

            for(int y = 0; y < openList.Count; y++) 
            {
                A_StarNode openNode = (A_StarNode)openList[y];
                if(openNode.f < pieninLoydettyFarvo)
                {
                    pieninLoydettyFarvo = openNode.f;
                    pieninFarvo = openNode;
                }
            }
            // tää koko osio haisee...
            kasiteltavaNode = pieninFarvo;
            Debug.Log(kasiteltavaNode);

            yield return new WaitForSeconds(1f);

            //tässä oli mätää kasiteltavanode.loppupiste == true
            if(kasiteltavaNode.loppupiste == true)
            {
                Debug.Log("Loppupiste löytyi!");
                break;
            }

            if(openList.Count < 0 )
            {
                Debug.Log("Reittiä ei löytynyt");
            }
        }
        yield return new WaitForSeconds(0.5f);
    
        A_StarNode reittiNode = lopetusNode;

        for(int y = 0;y < openList.Count;y++) 
        {
            
            reittiNode.VaihdaVari(NodeVarit.Reitti);
            reittiNode = reittiNode.vanhempi;
        }
    }
    void KasitellaanNode(A_StarNode node, A_StarNode[] naapurit)
    {
        for (int i = 0; i < naapurit.Length; i++)
        {
            A_StarNode naapuri = naapurit[i];

            bool naapuriOliClosedListassa = false;

            for (int x = 0; x < closeList.Count; x++)
            {
                A_StarNode closedListNode = (A_StarNode)closeList[x];

                if (naapuri.name == closedListNode.name)
                {
                    naapuriOliClosedListassa = true;
                    x = closeList.Count;
                }
            }

            bool naapuriOliOpenListassa = false;
            if (!naapuriOliClosedListassa && !naapuri.m_este)
            {
                for (int p = 0; p < openList.Count; p++)
                {
                    A_StarNode openListNode = (A_StarNode)openList[p];
                    if (openListNode.name == naapuri.name)
                    {
                        naapuriOliOpenListassa = true;
                        break;
                    }
                }

                if (!naapuriOliOpenListassa)
                {
                    openList.Add(naapuri);
                    naapuri.VaihdaVari(NodeVarit.openList);
                    naapuri.g = node.g + 1f;
                    naapuri.vanhempi = node;

                    
                }
                else
                {
                    // Update the node's parent if this path is shorter
                    if (node.g + 1 < naapuri.g)
                    {
                        naapuri.g = node.g + 1f;
                        naapuri.vanhempi = node;
                    }
                }

                // Calculate the Manhattan distance properly
                float manhattanX = lopetusNode.transform.position.x - naapuri.transform.position.x;
                float manhattanZ = lopetusNode.transform.position.z - naapuri.transform.position.z;
        
                naapuri.h = Mathf.Abs(manhattanX) + Mathf.Abs(manhattanZ);



                // Recalculate the total cost f
                naapuri.f = naapuri.g + naapuri.h;
            }
        }
    }

}
