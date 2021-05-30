using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public GameObject Building;
    public GameObject Storage;
    public GameObject HPbar;
    public GameObject Builder;
    public GameObject Soldier;
    public GameObject Hospital;
    public GameObject Barracks;
    public GameObject Warrior;
    public GameObject RangeUnit;
    public Dictionary<string, GameObject> ListOFBuildings = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> ListOFUnits = new Dictionary<string, GameObject>();

    public Material team1;
    public  Material team2;
    // Start is called before the first frame update
    void Start()
    {




        HPbar = Resources.Load<GameObject>("Prefab/HP") as GameObject;
        Building = Resources.Load<GameObject>("Prefab/MainBuilding") as GameObject;
        Builder = Resources.Load<GameObject>("Prefab/Builder") as GameObject;
        Soldier = Resources.Load<GameObject>("Prefab/Soldier") as GameObject;
        Storage = Resources.Load<GameObject>("Prefab/Storage") as GameObject;
        Hospital = Resources.Load<GameObject>("Prefab/Hospital") as GameObject;
        Barracks = Resources.Load<GameObject>("Prefab/Barracks") as GameObject;
        Warrior = Resources.Load<GameObject>("Prefab/Warrior") as GameObject;
        RangeUnit = Resources.Load<GameObject>("Prefab/RangeUnit") as GameObject;

        team1 = Resources.Load<Material>("Material/Team1") as Material;
        team2 = Resources.Load<Material>("Material/Team2") as Material;




        ListOFBuildings["Building"] = Building;
        ListOFBuildings["Storage"] = Storage;
        ListOFBuildings["Hospital"] = Hospital;
        ListOFBuildings["Barracks"] = Barracks;
        ListOFUnits[Builder.GetComponent<Unit>().UnitType] = Builder;
        ListOFUnits[Soldier.GetComponent<Unit>().UnitType] = Soldier;
        ListOFUnits[Warrior.GetComponent<Unit>().UnitType] = Warrior;
        ListOFUnits[RangeUnit.GetComponent<Unit>().UnitType] = RangeUnit;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
