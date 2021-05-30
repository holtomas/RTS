using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoMine : MonoBehaviour
{
    private HashSet<Building> Buildings = new HashSet<Building>();

    // Start is called before the first frame update
    void Start()
    {

        foreach (var building in FindObjectsOfType<Building>())
        {
            Buildings.Add(building);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Unit>().mining)
        {
            Buildings.Clear();
            AutoMining(transform);
        }
    }

    public void AutoMining(Transform unit)
    {
        if (unit.GetComponent<Unit>().Backpack == unit.GetComponent<Unit>().BackpackSize)
        {
            if (unit.GetComponent<NavMeshObstacle>().enabled)
            {
                unit.GetComponent<NavMeshObstacle>().enabled = false;
                unit.GetComponent<NavMeshAgent>().enabled = true;
            }

            foreach (var building in FindObjectsOfType<Building>())
            {
                Buildings.Add(building);
                if (building.GetComponentInChildren<BuildingRepairSize>().Storage && building.Completed)
                {
                    if (transform.GetComponent<Unit>().StoragePoint == null)
                    {
                        transform.GetComponent<Unit>().StoragePoint = building.gameObject;
                    }
                    else
                    {
                        
                        if (Vector3.Distance(transform.position, transform.GetComponent<Unit>().StoragePoint.transform.position) >
                            Vector3.Distance(transform.position, building.transform.position))
                        {
                            
                            transform.GetComponent<Unit>().StoragePoint = building.gameObject;
                        }
                    }
   
                }
               
            }

            
            if (transform.GetComponent<Unit>().StoragePoint != null)
            {
                
                transform.GetComponent<NavMeshAgent>().SetDestination(transform.GetComponent<Unit>().StoragePoint.transform.position);
            }
   
        }
    }
}
