using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Unitsmovement : MonoBehaviour
{

    public GameControls gameControls;
    private Camera cam;
    public LayerMask groudLayer;
    private RaycastHit hit;

    private void Awake()
    {
        cam = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        

        if (gameControls.selectedUnits != null && Input.GetMouseButtonDown(1))
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(camRay, out hit, 5000f, -1, QueryTriggerInteraction.Ignore))
            {
                
                if (hit.transform.CompareTag("Ground") && !GetComponent<BuildBuilding>().activeBuilding)
                {
                    int x = 0;
                    int z = 0;

                    foreach (var gameobject in gameControls.selectedUnits)
                    {
                        if (x == 4)
                        {
                            x = 0;
                            z++;
                        }


                        if (!IsPointerOverUIObject())
                        {
                            gameobject.GetComponent<Unit>().Attacking = false;
                            gameobject.GetComponent<Unit>().Enemy = null;
                            if (gameobject.GetComponent<NavMeshObstacle>().enabled)
                            {
                                
                                gameobject.GetComponent<NavMeshObstacle>().enabled = false;
                                gameobject.GetComponent<NavMeshAgent>().enabled = true;
                            }
                            gameobject.GetComponent<Unit>().RepairActive = false;
                            gameobject.GetComponent<Unit>().mining = false;
                            gameobject.GetComponent<Unit>().selectedBuilding = null;
                            gameobject.GetComponent<NavMeshAgent>().SetDestination(GetPoint() + new Vector3(x * 3.5f, 0, z * 3.5f));
                            gameobject.GetComponent<Unit>().MovingTo = GetPoint() + new Vector3(x * 3.5f, 0, z * 3.5f);
                        }

                        x++;
                    }

                }
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



    public Vector3 GetPoint()
    {

        Vector2 screenPosition = Input.mousePosition;
        Vector3 mousePosition = cam.ScreenToWorldPoint(screenPosition);

        RaycastHit hit;

        Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100f, groudLayer);

        return hit.point;

    }
}
