using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HospitalUI : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject HospitalPanel;
    GameObject HospitalButton;
    GameObject HospitalText;
    public ResUI resUI;
    public UI ui;
    

    public float GoldCoSt;
    public float WoodCoSt;


    void Start()
    {
        resUI = GetComponent<ResUI>();
        ui = GetComponent<UI>();
        HospitalPanel = new GameObject();
        HospitalText = new GameObject();
        HospitalButton = new GameObject();
        

        ui.CreatePanel(
                    HospitalPanel,
                   "HospitalPanel",
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

        ui.createButton(HospitalButton,
                        HospitalPanel,
                        "WorkerButton",
                        new Vector3(0f, 0f, 0f),
                        new Vector2(1f, 1f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f));

        HospitalButton.GetComponent<Button>().onClick.AddListener(() => { Heal(); });

        ui.CreateText(HospitalText,
                    HospitalButton,
                    "Heal Cost per Unit " +
                    " 10W " +
                    "10G",
                    ui.ArialFont,
                    new Vector2(1f, 1f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f)
                    );

    }

    private void Heal()
    {
        foreach (var unit in FindObjectsOfType<Unit>())
        {
            if (unit.Team == 1 && resUI.Gold >= GoldCoSt &&
                resUI.Wood >= WoodCoSt &&
                unit.Health != unit.Maxhealth &&
                GetComponent<GameControls>().SelectedBuilding.GetComponent<Building>().Completed) 
            {
                unit.Health = unit.Maxhealth;
                resUI.Gold -= GoldCoSt;
                resUI.Wood -= WoodCoSt;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
