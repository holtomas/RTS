using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class RepairBuilding : MonoBehaviour
{
    private RaycastHit hit;
    private List<Transform> selectedUnits = new List<Transform>();
    private BuildBuilding buildbuilding;

    private GameObject ClosestRes;
    private Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponentInParent<Camera>();
        selectedUnits = transform.GetComponent<GameControls>().selectedUnits;
        buildbuilding = transform.GetComponent<BuildBuilding>();
}

    // Update is called once per frame
    void Update()
    {
        selectedUnits = transform.GetComponent<GameControls>().selectedUnits;

        if (Input.GetMouseButtonDown(1) && selectedUnits != null && !buildbuilding.activeBuilding)
        {
            Ray camRay = camera.ScreenPointToRay(Input.mousePosition);

            if (!IsPointerOverUIObject())
            {
                if (Physics.Raycast(camRay, out hit, 5000f, -1, QueryTriggerInteraction.Ignore))
                {
                    if (hit.transform.CompareTag("Building") && hit.transform.GetComponent<Building>().team == 1)
                    {
                       
                        Repair(selectedUnits, hit.transform.gameObject);

                    }

                    if (hit.transform.CompareTag("Resource"))
                    {
                        Mine(selectedUnits, hit.transform.gameObject);
                    }

                    if (hit.transform.CompareTag("Unit"))
                    {
                        foreach (var unit in selectedUnits)
                        {
                            if (unit.GetComponent<Unit>().UnitType != "Builder")
                            {
                                unit.GetComponent<Unit>().Enemy = hit.transform.gameObject;
                                if (!unit.GetComponent<NavMeshAgent>().enabled)
                                {
                                    unit.GetComponent<NavMeshObstacle>().enabled = false;
                                    unit.GetComponent<NavMeshAgent>().enabled = true;
                                }
                                unit.GetComponent<NavMeshAgent>().SetDestination(hit.transform.position);
                                unit.GetComponent<Unit>().Attacking = true;
                            }
                        };


                    }

                    if (hit.transform.CompareTag("Building"))
                    {
                        foreach (var unit in selectedUnits)
                        {
                            if (unit.GetComponent<Unit>().UnitType != "Builder")
                            {
                                unit.GetComponent<Unit>().Enemy = hit.transform.gameObject;
                                if (!unit.GetComponent<NavMeshAgent>().enabled)
                                {
                                    unit.GetComponent<NavMeshObstacle>().enabled = false;
                                    unit.GetComponent<NavMeshAgent>().enabled = true;
                                }
                                unit.GetComponent<NavMeshAgent>().SetDestination(hit.transform.position);
                                unit.GetComponent<Unit>().Attacking = true;
                            }
                        };


                    }
                }
            }

        }

       if (Input.GetMouseButtonDown(1) &&
            transform.GetComponent<GameControls>().SelectedBuilding != null &&
            !buildbuilding.activeBuilding 
            )
       {
            
            if (transform.GetComponent<GameControls>().SelectedBuilding.GetComponent<Building>().Selected)
            {
                transform.GetComponent<GameControls>().SelectedBuilding.GetComponent<Building>().RallyPoint = transform.GetComponent<Unitsmovement>().GetPoint();
            }
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


    public void Repair(List<Transform> selectedUnits, GameObject building)
    {
        foreach (var unit in selectedUnits)
        {
            if (unit.GetComponent<Unit>().UnitType != "Builder")
            {
                unit.GetComponent<NavMeshAgent>().SetDestination(unit.transform.position);
            }
            else 
            {
                clearstats(unit);
                unit.GetComponent<Unit>().selectedBuilding = building;
                unit.GetComponent<NavMeshAgent>().SetDestination(building.transform.position);
                unit.GetComponent<Unit>().RepairActive = true;
            }
        }
    }

    public void Mine(List<Transform> selectedUnits, GameObject resource)
    {
        foreach (var unit in selectedUnits)
        {
            ClosestRes = null;
            if (unit.GetComponent<Unit>().UnitType != "Builder")
            {
                unit.GetComponent<NavMeshAgent>().SetDestination(unit.transform.position);
            }
            else if (resource.GetComponent<GoldRes>().miners.Count != resource.GetComponent<GoldRes>().MaxMiners)
            {
                clearstats(unit);
                unit.GetComponent<Unit>().selectedBuilding = resource;

                unit.GetComponent<NavMeshAgent>().SetDestination(resource.transform.position);
                unit.GetComponent<Unit>().MinePoint = resource.transform.position;
                unit.GetComponent<Unit>().mining = true;
                resource.GetComponent<GoldRes>().miners.Add(unit.gameObject);
            }
            else
            {
                
                foreach (var res in FindObjectsOfType<GoldRes>())
                {

                    if (res.GetComponent<GoldRes>().wood &&
                        resource.GetComponent<GoldRes>().wood &&
                        res.gameObject != resource.transform.gameObject &&
                        res.GetComponent<GoldRes>().miners.Count < res.GetComponent<GoldRes>().MaxMiners)
                    {
                        if (ClosestRes == null)
                        {
                            ClosestRes = res.gameObject;
                           

                        }
                        else if (Vector3.Distance(resource.transform.position, ClosestRes.transform.position) >
                                Vector3.Distance(resource.transform.position, res.transform.position))
                        {
                            ClosestRes = res.gameObject;
                           
                        }
                    }
                }

                if (ClosestRes != null)
                {
                    clearstats(unit);
                    unit.GetComponent<Unit>().selectedBuilding = ClosestRes;

                    unit.GetComponent<NavMeshAgent>().SetDestination(ClosestRes.transform.position);
                    unit.GetComponent<Unit>().MinePoint = ClosestRes.transform.position;
                    unit.GetComponent<Unit>().mining = true;
                    ClosestRes.GetComponent<GoldRes>().miners.Add(unit.gameObject);
                    
                }
                else
                {
                    clearstats(unit);
                    unit.GetComponent<NavMeshAgent>().SetDestination(unit.transform.position);
                }
            }
        }
    }

    

    public void clearstats(Transform unit)
    {
        if (unit.GetComponent<Unit>().mining && unit.GetComponent<Unit>().selectedBuilding != null)
        {
            unit.GetComponent<Unit>().selectedBuilding.GetComponent<GoldRes>().miners.Remove(unit.gameObject);
            unit.GetComponent<Unit>().mining = false;
        }
            
            unit.GetComponent<Unit>().RepairActive = false;

            if (unit.GetComponent<NavMeshObstacle>().enabled)
            {
                unit.GetComponent<NavMeshObstacle>().enabled = false;
                unit.GetComponent<NavMeshAgent>().enabled = true;
            }
        
    }

   
}
