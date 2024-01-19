using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject m_ghostPrefab;

    // Lista spawning positioille
    private List<Vector3> positions = new List<Vector3>();

    //spawn type
    public enum SpawnType {OneByOne}

    public SpawnType spawnType;
    
   // Kuinka monta spawnataan
    public int numberOfObjectsToSpawnOnContact = 10;
    public int maxGameobjectsToSpawn = 10;

    private int nextSpawnPointIndex = 0;
    private int spawnedObjects = 0;
   
    void Start()
    {
        //ker‰t‰‰n listaan kaikki hauta gameobjectit
        for (int i = 1; i < 11; i++)
        {
            GameObject enemy = GameObject.Find($"enemyGrave{i}");
            //Vector3 points = new Vector3(enemy.transform.position.x, 2f, enemy.transform.position.z);
            Vector3 points = enemy.transform.position;
            positions.Add(points);
        }
        
    }

    void Update()
    {
        
    }
    // Jos pelaaja osuu tyhj‰‰n esineeseen, jossa scripti on kiinni, se kutsuu metodia, joka tekee vihollisia
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Syntyk‰‰ demonini!");
        if (col.tag == "Player") 
        {
            SpawnAnObject();
        }
    }
    // Toimii vain yhden kerran
    private void OnTriggerExit(Collider other)
    {
        gameObject.SetActive(false);
    }
    // Metodi vihollisten spawnaamiselle
    private void SpawnAnObject()
    {
        if (spawnedObjects >= maxGameobjectsToSpawn)    
            return;

        for (int i = 0; i < numberOfObjectsToSpawnOnContact; i++)
        {   
            // Default spawn point on ensimm‰inen positio listassa
            Vector3 spawnPoint = positions[0]; 

            if (spawnType == SpawnType.OneByOne)
            {
                spawnPoint = positions[nextSpawnPointIndex];
                nextSpawnPointIndex++;
                if (nextSpawnPointIndex >= positions.Count)
                    nextSpawnPointIndex = 0;
            }
            // Spawnataan kummitukset niiden lista positioille
            GameObject prefabi = Instantiate(m_ghostPrefab, spawnPoint, Quaternion.identity);
            prefabi.transform.position = spawnPoint + new Vector3(2f,2f,1f);
            spawnedObjects++;  
            
        }
    }
}

