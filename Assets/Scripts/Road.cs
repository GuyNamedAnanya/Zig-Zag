using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] GameObject roadPrefab;

    [SerializeField] Vector3 lastPosition;

    [SerializeField] float offset;

    int roadCount = 0;
    public void StartCreating()
    {
        InvokeRepeating("ProcedurallyCreateWorld", 1f, 0.2f);
    }
    // Start is called before the first frame update
    void ProcedurallyCreateWorld()
    {
        Vector3 spawnPos = Vector3.zero;

        int chance = Random.Range(0, 100);

        if(chance < 50)
        {
            spawnPos = new Vector3(lastPosition.x + offset, lastPosition.y, lastPosition.z + offset); 
        }
        else
        {
            spawnPos = new Vector3(lastPosition.x - offset, lastPosition.y, lastPosition.z + offset);
        }

        GameObject nextRoadPart = Instantiate(roadPrefab, spawnPos, Quaternion.Euler(0, 45, 0));

        lastPosition = nextRoadPart.transform.position;

        roadCount++;

        if(roadCount % 5 == 0)
        {
            nextRoadPart.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

   
}
