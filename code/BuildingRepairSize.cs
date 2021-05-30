using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BuildingRepairSize : MonoBehaviour
{
    public int builders = 0;
    public int MaxBuilders = 5;
    public bool Storage = false;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {   
        GetComponent<Collider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (builders > 0 && this.transform.GetComponentInParent<Building>().health < this.transform.GetComponentInParent<Building>().healthMax)
        {
            for (int i = 0; i < builders; i++)
            {
                this.transform.GetComponentInParent<Building>().health += 0.2f;
            }
            if (this.transform.GetComponentInParent<Building>().health >= this.transform.GetComponentInParent<Building>().healthMax)
            {
                this.transform.GetComponentInParent<Building>().Completed = true;
            }
        }
       
    }


    private void OnTriggerEnter(Collider unit)
    {

       
        if (unit.GetComponent<Unit>() != null)
        {
            
            if (unit.GetComponent<Unit>().UnitType == "Builder" &&
                unit.GetComponent<Unit>().RepairActive &&
                unit.GetComponent<Unit>().selectedBuilding == GetComponentInParent<Building>().gameObject &&
                builders <= MaxBuilders
                )
            {
                
                unit.GetComponent<NavMeshAgent>().SetDestination(unit.transform.position);
                builders++;
                unit.GetComponent<NavMeshAgent>().enabled = false;
                unit.GetComponent<NavMeshObstacle>().enabled = true;
                unit.GetComponent<Unit>().Build = true;
                if (Storage && GetComponentInParent<Building>().Completed)
                {
                    cam.GetComponentInChildren<ResUI>().Gold += unit.GetComponent<Unit>().GoldBack;
                    cam.GetComponentInChildren<ResUI>().Wood += unit.GetComponent<Unit>().WoodBack;
                    unit.GetComponent<Unit>().Backpack = 0;
                    unit.GetComponent<Unit>().GoldBack = 0;
                    unit.GetComponent<Unit>().WoodBack = 0;
                }
                


            }
           

            
            
               else if (unit.GetComponent<Unit>().StoragePoint != null)
                {
                    if (unit.GetComponent<Unit>().UnitType == "Builder" &&
                         unit.GetComponent<Unit>().StoragePoint.GetComponentInChildren<BuildingRepairSize>().gameObject == transform.gameObject &&
                            Storage &&
                           unit.GetComponent<Unit>().mining
                        )
                     {
                          cam.GetComponentInChildren<ResUI>().Gold += unit.GetComponent<Unit>().GoldBack;
                          cam.GetComponentInChildren<ResUI>().Wood += unit.GetComponent<Unit>().WoodBack;
                          unit.GetComponent<Unit>().Backpack = 0;
                          unit.GetComponent<Unit>().GoldBack = 0;
                          unit.GetComponent<Unit>().WoodBack = 0;
                          unit.GetComponent<NavMeshAgent>().SetDestination(unit.GetComponent<Unit>().MinePoint);
                     }
                }
               

            
        }
    }
  /*  private void OnTriggerStay(Collider unit)
    {
        if (unit.GetComponent<Unit>() != null)
        {
            if (unit.GetComponent<Unit>().StoragePoint != null)
            {
                if (unit.GetComponent<Unit>().UnitType == "Builder" &&
                    unit.GetComponent<Unit>().StoragePoint.GetComponentInChildren<BuildingRepairSize>().gameObject == transform.gameObject &&
                    Storage &&
                    unit.GetComponent<Unit>().mining

                    )
                {
                    unit.GetComponent<Unit>().Backpack = 0;
                    unit.GetComponent<Unit>().GoldBack = 0;
                    unit.GetComponent<Unit>().WoodBack = 0;
                    unit.GetComponent<NavMeshAgent>().SetDestination(unit.GetComponent<Unit>().MinePoint);
                }
            }
           
        }
        
    }
    */
    private void OnTriggerExit(Collider unit)
    {

        if (unit.GetComponent<Unit>() != null)
        {
            if (unit.GetComponent<Unit>().UnitType == "Builder" && builders != 0 && unit.GetComponent<Unit>().Build)
            {
                unit.GetComponent<Unit>().Build = false;
                builders--;
            }
        }
    }
}
