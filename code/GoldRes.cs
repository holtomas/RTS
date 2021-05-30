using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoldRes : MonoBehaviour
{

    public float Amount;
    public float miner = 0;
    public float MaxMiners = 6;

    public bool wood = false;
    public bool gold = false;

    private GameObject ClosestRes;
    public HashSet<GameObject> miners = new HashSet<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        miner = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider unit)
    {

        if (unit.GetComponent<Unit>() != null && miners.Count <= MaxMiners && unit.GetComponent<Unit>().selectedBuilding == transform.gameObject)
        {

            if (unit.GetComponent<Unit>().UnitType == "Builder" &&
                unit.GetComponent<Unit>().mining &&
                unit.GetComponent<Unit>().selectedBuilding == transform.gameObject
                )
            {

                if (unit.GetComponent<Unit>().Backpack < unit.GetComponent<Unit>().BackpackSize)
                {
                    unit.GetComponent<NavMeshAgent>().SetDestination(unit.transform.position);     
                    unit.GetComponent<NavMeshAgent>().enabled = false;
                    unit.GetComponent<NavMeshObstacle>().enabled = true;


                   
                    StartCoroutine(Speed(unit.GetComponent<Unit>().MineperSec, unit.transform));
                    

                }
               

                


            }
        }
    }

    private void OnTriggerExit(Collider unit)
    {
        if (miners.Contains(unit.gameObject) && !unit.GetComponent<Unit>().mining && unit.GetComponent<Unit>().MinePoint == transform.position)
        {

            miners.Remove(unit.gameObject);
          //  unit.GetComponent<Unit>().mining = false;
        }
            
    }
    private void OnTriggerStay(Collider unit)
    {
        if (unit.GetComponent<Unit>() != null)
        {
            if (unit.GetComponent<Unit>().UnitType == "Builder" &&
                unit.GetComponent<Unit>().mining &&
                unit.GetComponent<Unit>().MinePoint == transform.position &&
                miners.Count == MaxMiners
               )
            {
                miners.Add(unit.gameObject);
                

            }
            

        }


        
    }




    public IEnumerator Speed(float speed, Transform unit)
    {
        yield return new WaitForSeconds(speed);

        while (unit.GetComponent<Unit>().mining && unit.GetComponent<Unit>().Backpack != unit.GetComponent<Unit>().BackpackSize && !unit.GetComponent<Unit>().moving)
        {
           
            Amount--;
            if (gold)
            {
                unit.GetComponent<Unit>().GoldBack++;

            }
            if (wood)
            {
                unit.GetComponent<Unit>().WoodBack++;

            }

            unit.GetComponent<Unit>().Backpack++;

            if (Amount <= 0)
            {
                
                foreach (var res in FindObjectsOfType<GoldRes>())
                {
                    
                    if (res.GetComponent<GoldRes>().gold &&
                        gold && res.gameObject != transform.gameObject &&
                        res.GetComponent<GoldRes>().miners.Count < res.GetComponent<GoldRes>().MaxMiners)
                    {
                        if (ClosestRes == null)
                        {
                            ClosestRes = res.gameObject;

                        }
                        else if (Vector3.Distance(transform.position, ClosestRes.transform.position) >
                                Vector3.Distance(transform.position, res.transform.position))
                         {

                               ClosestRes = res.gameObject;
                         }
                    }

                         if (res.GetComponent<GoldRes>().wood &&
                             wood &&
                             res.gameObject != transform.gameObject &&
                             res.GetComponent<GoldRes>().miners.Count < res.GetComponent<GoldRes>().MaxMiners
                             )
                         {
                        
                                if (ClosestRes == null)
                                {
                            
                            ClosestRes = res.gameObject;
                        }
                        else if (Vector3.Distance(transform.position, ClosestRes.transform.position) >
                                Vector3.Distance(transform.position, res.transform.position))
                        {
                            
                            ClosestRes = res.gameObject;
                        }
                    }

                }
                foreach (var miner in miners)
                {
                    if (ClosestRes != null)
                    {
                        miner.GetComponent<Unit>().selectedBuilding = ClosestRes;
                        miner.GetComponent<Unit>().MinePoint = ClosestRes.transform.position;
                        MoveToMinePoint(miner, ClosestRes.transform.position);
                    }
                    else
                    {
                        miner.GetComponent<Unit>().selectedBuilding = null;
                    }
                                    }
                Destroy(this.gameObject);
            }
            yield return new WaitForSeconds(speed);
            


        }

       
    }

    public void MoveToMinePoint(GameObject unit, Vector3 Point)
    {
        unit.GetComponent<NavMeshObstacle>().enabled = false;
        unit.GetComponent<NavMeshAgent>().enabled = true;

        unit.GetComponent<NavMeshAgent>().SetDestination(Point);
    }


}
