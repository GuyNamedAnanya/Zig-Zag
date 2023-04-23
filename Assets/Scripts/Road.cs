using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] GameObject roadPrefab;

    [SerializeField] Vector3 lastPosition;

    [SerializeField] float offset;

    float angleOfRoad = 45f;
    float diamondSpawnNum = 5f;
    int minChance = 0;
    int maxChance = 100;
    int roadCount = 0;
    public void StartCreating()
    {
        InvokeRepeating("ProcedurallyCreateWorld", 1f, 0.2f);
    }

    /// <summary>
    /// Procedurally generates roads pieces 
    /// </summary>
    void ProcedurallyCreateWorld()
    {
        Vector3 spawnPos = Vector3.zero;

        //chance for getting left or right piece
        int chance = Random.Range(minChance, maxChance);

        //50-50 chance fo getting left or right road piece
        if(chance < 50)
        {
            spawnPos = new Vector3(lastPosition.x + offset, lastPosition.y, lastPosition.z + offset); 
        }
        else
        {
            spawnPos = new Vector3(lastPosition.x - offset, lastPosition.y, lastPosition.z + offset);
        }

        //instantiates next road piece 
        GameObject nextRoadPart = Instantiate(roadPrefab, spawnPos, Quaternion.Euler(0, angleOfRoad, 0));

        lastPosition = nextRoadPart.transform.position;

        roadCount++;

        //if roadcount is divisible by diamond spawn num then diamond gets spawned
        if(roadCount % diamondSpawnNum == 0)
        {
            nextRoadPart.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

   
}
