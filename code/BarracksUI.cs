using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarracksUI : MonoBehaviour
{
    GameObject BarracksPanel;

    GameObject WarriorButton;
    GameObject WarriorText;

    GameObject RangeUnitButton;
    GameObject RangeUnitText;

    public ResUI resUI;
    public UI ui;
    public MainBuilding mainBuilding;

    // Start is called before the first frame update
    void Start()
    {
        resUI = GetComponent<ResUI>();
        ui = GetComponent<UI>();
        mainBuilding = GetComponent<MainBuilding>();

        BarracksPanel = new GameObject();

        WarriorButton = new GameObject();
        WarriorText = new GameObject();

        RangeUnitButton = new GameObject();
        RangeUnitText = new GameObject();

        ui.CreatePanel(
                    BarracksPanel,
                   "BarracksPanel",
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

        ui.createButton(WarriorButton,
                       BarracksPanel,
                       "WarriorButton",
                       new Vector3(0f, 0f, 0f),
                       new Vector2(0.5f, 1f),
                       new Vector2(0f, 0f),
                       new Vector2(0f, 0f),
                       new Vector2(0f, 0f),
                       new Vector2(0f, 0f));

        WarriorButton.GetComponent<Button>().onClick.AddListener(() => { mainBuilding.RecruitUnit(GetComponent<Prefabs>().ListOFUnits["Warrior"]); });

        ui.createButton(RangeUnitButton,
                        BarracksPanel,
                        "RangeUnitButton",
                        new Vector3(0f, 0f, 0f),
                        new Vector2(1f, 1f),
                        new Vector2(0.5f, 0f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f));

        RangeUnitButton.GetComponent<Button>().onClick.AddListener(() => { mainBuilding.RecruitUnit(GetComponent<Prefabs>().ListOFUnits["RangeUnit"]); });

        ui.CreateText(WarriorText,
                   WarriorButton,
                   "Warrior " +
                   " 50W " +
                   "100G",
                   ui.ArialFont,
                   new Vector2(1f, 1f),
                   new Vector2(0f, 0f),
                   new Vector2(0f, 0f),
                   new Vector2(0f, 0f)
                   );

        ui.CreateText(RangeUnitText,
                   RangeUnitButton,
                   "Range Unit " +
                   " 100W " +
                   "50G",
                   ui.ArialFont,
                   new Vector2(1f, 1f),
                   new Vector2(0f, 0f),
                   new Vector2(0f, 0f),
                   new Vector2(0f, 0f)
                   );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
