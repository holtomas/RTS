using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MainBuilding : MonoBehaviour
{
    // Start is called before the first frame update
    public UI ui;
    public GameObject MainBuildingPanel;
    public GameObject MainBuildingSubPanel;
    public GameObject WorkerButton;
    public GameObject TextWorkerButton;

    public GameObject SoldierButton;
    public GameObject TextSoldierButton;
    public float RecruitSec = 1f;

    public ResUI resUI;
    

    void Start()
    {

        resUI = GetComponent<ResUI>();
        ui = GetComponent<UI>();
        MainBuildingPanel = new GameObject();
        MainBuildingSubPanel = new GameObject();
        WorkerButton = new GameObject();
        TextWorkerButton = new GameObject();

        SoldierButton = new GameObject();
        TextSoldierButton = new GameObject();

        ui.CreatePanel(
                    MainBuildingPanel,
                   "MainBuildingPanel",
                    ui.myGO,
                    ui.UIbackgroud,
                    0.5f,
                    new Vector3(0f, 0f, 0f),
                    new Vector2(0.30f, 0.30f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    ui);

        ui.createButton(WorkerButton,
                        MainBuildingPanel,
                        "WorkerButton",
                        new Vector3(0f, 0f, 0f),
                        new Vector2(0.5f, 1f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f));

        WorkerButton.GetComponent<Button>().onClick.AddListener(() => {RecruitUnit(GetComponent<Prefabs>().ListOFUnits["Builder"]); });

        ui.createButton(SoldierButton,
                        MainBuildingPanel,
                        "SoldierButton",
                        new Vector3(0f, 0f, 0f),
                        new Vector2(1f, 1f),
                        new Vector2(0.5f, 0f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f));

        SoldierButton.GetComponent<Button>().onClick.AddListener(() => { RecruitUnit(GetComponent<Prefabs>().ListOFUnits["Soldier"]); });

        ui.CreateText(TextWorkerButton,
                      WorkerButton,
                     "Worker  10W  50G",
                     ui.ArialFont,
                     new Vector2(1f, 1f),
                     new Vector2(0f, 0f),
                     new Vector2(0f, 0f),
                     new Vector2(0f, 0f)
                     );

        ui.CreateText(TextSoldierButton,
                      SoldierButton,
                     "Peasant   10W  50G",
                     ui.ArialFont,
                     new Vector2(1f, 1f),
                     new Vector2(0f, 0f),
                     new Vector2(0f, 0f),
                     new Vector2(0f, 0f)
                     );

    }

    public void RecruitUnit(GameObject unit)
    {
       
        GameObject building = GetComponent<GameControls>().SelectedBuilding;
        building.GetComponent<Building>().SpawnPoint = building.GetComponent<Building>().Spawn.transform.position;
        if (building.GetComponent<Building>().RallyPoint != new Vector3(0f,0f,0f) &&
            building.GetComponent<Building>().Completed &&
            resUI.CurrentUnits < resUI.MaximumUnits &&
            resUI.Wood >= unit.GetComponent<Unit>().WoodCost &&
            resUI.Gold >= unit.GetComponent<Unit>().GoldCost
            )
        {
            building.GetComponent<Building>().Clicks++;
            resUI.Wood -= unit.GetComponent<Unit>().WoodCost;
            resUI.Gold -= unit.GetComponent<Unit>().GoldCost;
            if (!building.GetComponent<Building>().Recruiting)
            {
                StartCoroutine(Recruit(unit, building));
                building.GetComponent<Building>().Recruiting = true;
            }
        }
        

    }

    public IEnumerator Recruit(GameObject unit, GameObject building)
    {
        if (building.GetComponent<Building>().Clicks > 0 && resUI.CurrentUnits < resUI.MaximumUnits)
        {
            yield return new WaitForSeconds(RecruitSec);
            GameObject SpawnedUnit = Instantiate(unit, building.GetComponent<Building>().SpawnPoint, Quaternion.identity);

            SpawnedUnit.GetComponent<NavMeshAgent>().SetDestination(building.GetComponent<Building>().RallyPoint);
            building.GetComponent<Building>().Clicks--;
            StartCoroutine(Recruit(unit, building));
        }

        else
        {
            
            building.GetComponent<Building>().Recruiting = false;
        }
        
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
