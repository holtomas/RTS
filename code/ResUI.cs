using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResUI : MonoBehaviour
{
    public UI ui;
    public float Wood;
    public float Gold;
    public float units;
    public float MaximumUnits;
    public float CurrentUnits;

    public GameObject GoldPanel;
    public GameObject WoodPanel;
    public GameObject Units;

    public GameObject TextGoldPanel;
    public GameObject NummberGoldPanel;

    public GameObject TextWoodPanel;
    public GameObject NummberWoodPanel;

    public GameObject TextUnitsPanel;
    public GameObject NummberUnitsPanel;
    // Start is called before the first frame update

    private void Awake()
    {
        ui = GetComponent<UI>();
        GoldPanel = new GameObject();
        WoodPanel = new GameObject();
        Units = new GameObject();

        NummberGoldPanel = new GameObject();
        TextGoldPanel = new GameObject();

        NummberWoodPanel = new GameObject();
        TextWoodPanel = new GameObject();

        NummberUnitsPanel = new GameObject();
        TextUnitsPanel = new GameObject();

    }
    void Start()
    {
        Gold = 3300;
        Wood = 5100;


        ui.CreatePanel(GoldPanel,
                   "GoldPanel",
                    ui.myGO,
                    ui.UIbackgroud,
                    1f,
                    new Vector3(0f, 0f, 0f),
                    new Vector2(0.33f, 1f),
                    new Vector2(0f, 0.95f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    ui);
        GoldPanel.SetActive(true);


        ui.CreateText(TextGoldPanel,
                    GoldPanel,
                    "Gold: ",
                    ui.ArialFont,
                    new Vector2(0.5f, 1f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f));

        TextGoldPanel.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        TextGoldPanel.GetComponent<Text>().color = Color.white;

        ui.CreateText(NummberGoldPanel,
                    GoldPanel,
                    "Gold: ",
                    ui.ArialFont,
                    new Vector2(1f, 1f),
                    new Vector2(0.5f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f));

        NummberGoldPanel.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        NummberGoldPanel.GetComponent<Text>().color = Color.white;


        ui.CreatePanel(WoodPanel,
                   "WoodPanel",
                    ui.myGO,
                    ui.UIbackgroud,
                    1f,
                    new Vector3(0f, 0f, 0f),
                    new Vector2(0.66f, 1f),
                    new Vector2(0.33f, 0.95f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    ui);
        WoodPanel.SetActive(true);

        ui.CreateText(TextWoodPanel,
                    WoodPanel,
                    "Wood: ",
                    ui.ArialFont,
                    new Vector2(0.5f, 1f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f));

        TextWoodPanel.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        TextWoodPanel.GetComponent<Text>().color = Color.white;

        ui.CreateText(NummberWoodPanel,
                    WoodPanel,
                    "Gold: ",
                    ui.ArialFont,
                    new Vector2(1f, 1f),
                    new Vector2(0.5f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f));

        NummberWoodPanel.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        NummberWoodPanel.GetComponent<Text>().color = Color.white;


        ui.CreatePanel(Units,
                   "MaxUnits",
                    ui.myGO,
                    ui.UIbackgroud,
                    1f,
                    new Vector3(0f, 0f, 0f),
                    new Vector2(1f, 1f),
                    new Vector2(0.66f, 0.95f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    ui);
        Units.SetActive(true);

        ui.CreateText(TextUnitsPanel,
                   Units,
                   "Units: ",
                   ui.ArialFont,
                   new Vector2(0.5f, 1f),
                   new Vector2(0f, 0f),
                   new Vector2(0f, 0f),
                   new Vector2(0f, 0f));

        TextUnitsPanel.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        TextUnitsPanel.GetComponent<Text>().color = Color.white;

        ui.CreateText(NummberUnitsPanel,
                    Units,
                    "Gold: ",
                    ui.ArialFont,
                    new Vector2(1f, 1f),
                    new Vector2(0.5f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f));

        NummberUnitsPanel.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        NummberUnitsPanel.GetComponent<Text>().color = Color.white;

    }

    // Update is called once per frame
    void Update()
    {
        NummberGoldPanel.GetComponent<Text>().text = Gold.ToString();
        NummberWoodPanel.GetComponent<Text>().text = Wood.ToString();

        foreach (var item in FindObjectsOfType<Unit>())
        {
            units++;
        }
        NummberUnitsPanel.GetComponent<Text>().text = units.ToString() + " / " + MaximumUnits.ToString();
        CurrentUnits = units;
        units = 0;

    }
}
