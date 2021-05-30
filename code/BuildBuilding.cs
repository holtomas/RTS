using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class BuildBuilding : MonoBehaviour
{
    public Prefabs prefabs;
    GameObject CurrentBuilding = null;
    GameControls gameControls;
    RepairBuilding repairBuilding;
    public Camera camera;
    public ResUI resUI;
    public bool activeBuilding = false;
    
    // Start is called before the first frame update
    void Start()
    {
        resUI = GetComponent<ResUI>();
        camera = GetComponentInParent<Camera>();
        gameControls = GetComponent<GameControls>();
        repairBuilding = GetComponent<RepairBuilding>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentBuilding != null)
        {
            
            activeBuilding = true;
            MoveObjectToMouse();

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitinfo;
          


            if (Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out hitinfo, 5000f, -1, QueryTriggerInteraction.Ignore))
            {
                bool DistanceBuilding = true;
                bool DistanceResources = true;
                
                foreach (var building in FindObjectsOfType<Building>())
                {
                    if (Vector3.Distance(building.transform.position, CurrentBuilding.transform.position) < 4f && CurrentBuilding != building.gameObject)
                    {
                        DistanceBuilding = false;
                       
                    }
                }

                foreach (var res in FindObjectsOfType<GoldRes>())
                {

                    if (Vector3.Distance(res.transform.position, CurrentBuilding.transform.position) < 4f)
                    {
                        DistanceResources = false;
                        
                    }
                }


                if (hitinfo.transform.CompareTag("Ground") && DistanceBuilding && DistanceResources)
                {
                    foreach (var unit in gameControls.selectedUnits)
                    {
                        if (unit.GetComponent<Unit>().UnitType != "Builder")
                        {
                            unit.GetComponent<NavMeshAgent>().SetDestination(unit.transform.position);
                        }
                        else
                        {
                            repairBuilding.clearstats(unit);
                            unit.GetComponent<NavMeshAgent>().SetDestination(transform.GetComponent<Unitsmovement>().GetPoint());
                            unit.GetComponent<Unit>().RepairActive = true;
                            unit.GetComponent<Unit>().selectedBuilding = CurrentBuilding;
                        }
                    }
                    

                    CurrentBuilding.GetComponent<Collider>().enabled = true;
                    CurrentBuilding.GetComponent<NavMeshObstacle>().enabled = true;
                    CurrentBuilding.GetComponentInChildren<BuildingRepairSize>().enabled = true;

                    resUI.Wood -= CurrentBuilding.GetComponent<Building>().WoodCost;
                    resUI.Gold -= CurrentBuilding.GetComponent<Building>().GoldCost;

                    CurrentBuilding = null;
                    activeBuilding = false;
                }
                else
                {
                    foreach (var item in gameControls.selectedUnits)
                    {
                        if (item.GetComponent<NavMeshAgent>().enabled)
                        {
                            item.GetComponent<NavMeshAgent>().SetDestination(item.transform.position);
                        }
                       

                    }
                }
               
            }
            if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Escape))
            {
               

                Destroy(CurrentBuilding);
                CurrentBuilding = null;
                activeBuilding = false;
            }
        }
    }


    private void MoveObjectToMouse()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitinfo;
       
            if (Physics.Raycast(ray, out hitinfo, 5000f, -1, QueryTriggerInteraction.Ignore))
            {
                CurrentBuilding.transform.position = new Vector3(hitinfo.point.x, 1.5684f + hitinfo.point.y, hitinfo.point.z);

                
            }
        
        
    }



    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


    public void SetBuilding(string name)
    {
        if (prefabs.ListOFBuildings[name].GetComponent<Building>().GoldCost <= resUI.Gold &&
            prefabs.ListOFBuildings[name].GetComponent<Building>().WoodCost <= resUI.Wood) 
        {
            CurrentBuilding = Instantiate(prefabs.ListOFBuildings[name]);
            CurrentBuilding.GetComponentInChildren<BuildingRepairSize>().cam = camera;

        }
       

    }


}
