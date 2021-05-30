using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour
{

    public Font ArialFont;
    public GameObject BuilderPanel;
    public GameObject SoldierPanel;
    public GameObject WarriorPanel;
    public GameObject RangeUnitPanel;
    private float angleX = 70;
    public BuildBuilding Build;
    public Dictionary<string, GameObject> Panels;
    public Sprite UIbackgroud;
    public GameObject myGO;

    public GameObject TextBuilderButton;

    public ResUI resUI;

    public GameObject StorageButton;
    public GameObject TextStorageButton;

    public GameObject HospitalButton;
    public GameObject TextHospitalButton;

    public GameObject BarracsButton;
    public GameObject TextBarracsButton;

    public GameObject SoldierText;
    public GameObject WarriroText;
    public GameObject RangeUnitText;


   


    public void createButton(GameObject Button,
                             GameObject parent,
                             string name,
                             Vector3 position,
                             Vector2 anchorMax,
                             Vector2 anchorMin,
                             Vector2 pivot,
                             Vector2 offsetMax,
                             Vector2 offsetMin)
    {
        Button.transform.parent = parent.transform;
        Button.name = name;
        Button.AddComponent<RectTransform>();
        Button.AddComponent<Button>();
        Button.AddComponent<Image>();
        Button.GetComponent<Button>().targetGraphic = Button.GetComponent<Image>();
        Button.GetComponent<RectTransform>().anchorMax = anchorMax;
        Button.GetComponent<RectTransform>().anchorMin = anchorMin;
        Button.GetComponent<RectTransform>().pivot = pivot;
        Button.GetComponent<RectTransform>().position = position;
        Button.GetComponent<RectTransform>().offsetMax = offsetMax;
        Button.GetComponent<RectTransform>().offsetMin = offsetMin;
        
    }



    public void CreatePanel
                        (
                        GameObject obj,
                        string name,
                        GameObject parent,
                        Sprite sprite,
                        float transparency,
                        Vector3 position,
                        Vector2 anchorMax,
                        Vector2 anchorMin,
                        Vector2 pivot,
                        Vector2 offsetMax,
                        Vector2 offsetMin,
                        UI ui
                        )
    {
        
        obj.transform.parent = parent.transform;

        obj.name = name;
        obj.AddComponent<CanvasRenderer>();
        obj.AddComponent<RectTransform>();
        obj.AddComponent<Image>();
        obj.GetComponent<Image>().sprite = sprite;
        obj.GetComponent<Image>().color = new Color(0f, 0f, 0f, transparency);
        obj.GetComponent<RectTransform>().anchorMax = anchorMax;
        obj.GetComponent<RectTransform>().anchorMin = anchorMin;
        obj.GetComponent<RectTransform>().pivot = pivot;
        obj.GetComponent<RectTransform>().position = position;
        obj.GetComponent<RectTransform>().offsetMax = offsetMax;
        obj.GetComponent<RectTransform>().offsetMin = offsetMin;

        ui.Panels[name] = obj;
        obj.SetActive(false);

       
    }

    public void CreateSubPanel
                       (
                       GameObject obj,
                       string name,
                       GameObject parent,
                       Sprite sprite,
                       float transparency,
                       Vector3 position,
                       Vector2 anchorMax,
                       Vector2 anchorMin,
                       Vector2 pivot,
                       Vector2 offsetMax,
                       Vector2 offsetMin,
                       UI ui
                       )
    {

        obj.transform.parent = parent.transform;

        obj.name = name;
        obj.AddComponent<CanvasRenderer>();
        obj.AddComponent<RectTransform>();
        obj.AddComponent<Image>();
        obj.GetComponent<Image>().sprite = sprite;
        obj.GetComponent<Image>().color = new Color(0f, 0f, 0f, transparency);
        obj.GetComponent<RectTransform>().anchorMax = anchorMax;
        obj.GetComponent<RectTransform>().anchorMin = anchorMin;
        obj.GetComponent<RectTransform>().pivot = pivot;
        obj.GetComponent<RectTransform>().position = position;
        obj.GetComponent<RectTransform>().offsetMax = offsetMax;
        obj.GetComponent<RectTransform>().offsetMin = offsetMin;

        


    }


    public void CreateText(GameObject Text,
                           GameObject parent,
                           string name,
                           Font font,
                           Vector2 anchorMax,
                           Vector2 anchorMin,
                           Vector2 offsetMax,
                           Vector2 offsetMin)
    {
        Text.transform.parent = parent.transform;
        Text.name = name;
        

        Text text = Text.AddComponent<Text>();
        text.font = ArialFont;
        text.text = name;
        text.color = Color.black;
        text.fontSize = 24;
        text.alignment = TextAnchor.MiddleCenter;
       


        // Text position
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.anchorMax = anchorMax;
        rectTransform.anchorMin = anchorMin;
        rectTransform.offsetMax = offsetMax;
        rectTransform.offsetMin = offsetMin;


    }


    private void Awake()
    {
        Panels = new Dictionary<string, GameObject>();
        UIbackgroud = Resources.Load<Sprite>("Sprites/Stone") as Sprite;
        ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        myGO = new GameObject();
        resUI = GetComponent<ResUI>();

        Canvas myCanvas;





        // Canvas

        myGO.name = "TestCanvas";
        myGO.AddComponent<Canvas>();

        myCanvas = myGO.GetComponent<Canvas>();
        myGO.AddComponent<CanvasScaler>();
        myGO.AddComponent<GraphicRaycaster>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // vytvori eventsystem k fungovani tlacitka
        GameObject listener = new GameObject("EventSystem", typeof(EventSystem));
        listener.AddComponent<StandaloneInputModule>();
        listener.AddComponent<BaseInput>();
    }


    // Start is called before the first frame update
    void Start()
    {

        


        StorageButton = new GameObject();
        TextStorageButton = new GameObject();


        BuilderPanel = new GameObject();
        SoldierPanel = new GameObject();
        RangeUnitPanel = new GameObject();
        WarriorPanel = new GameObject();

        HospitalButton = new GameObject();
        TextHospitalButton = new GameObject();

        BarracsButton = new GameObject();
        TextBarracsButton = new GameObject();

        TextBuilderButton = new GameObject();

        SoldierText = new GameObject();
        WarriroText = new GameObject();
        RangeUnitText = new GameObject();





        
        

        // Text
     /*   myText = new GameObject();
        

        */


        //panel

        CreatePanel(
                    BuilderPanel,
                    "BuilderPanel",
                    myGO,
                    UIbackgroud,
                    0.5f,
                    new Vector3(0f, 0f, 0f),
                    new Vector2(0.30f, 0.30f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    GetComponent<UI>());
                    
        CreatePanel(
                   SoldierPanel,
                   "SoldierPanel",
                   myGO,
                   UIbackgroud,
                   0.5f,
                   new Vector3(0f, 0f, 0f),
                   new Vector2(0.30f, 0.30f),
                   new Vector2(0f, 0f),
                   new Vector2(0f, 0f),
                   new Vector2(0f, 0f),
                   new Vector2(0f, 0f),
                   GetComponent<UI>());

        CreatePanel(
                   WarriorPanel,
                   "WarriorPanel",
                   myGO,
                   UIbackgroud,
                   0.5f,
                   new Vector3(0f, 0f, 0f),
                   new Vector2(0.30f, 0.30f),
                   new Vector2(0f, 0f),
                   new Vector2(0f, 0f),
                   new Vector2(0f, 0f),
                   new Vector2(0f, 0f),
                   GetComponent<UI>());

        CreatePanel(
                   RangeUnitPanel,
                   "RangeUnitPanel",
                   myGO,
                   UIbackgroud,
                   0.5f,
                   new Vector3(0f, 0f, 0f),
                   new Vector2(0.30f, 0.30f),
                   new Vector2(0f, 0f),
                   new Vector2(0f, 0f),
                   new Vector2(0f, 0f),
                   new Vector2(0f, 0f),
                   GetComponent<UI>());

        SoldierPanel.SetActive(false);





        /*  BuilderPanel = new GameObject();
          BuilderPanel.transform.parent = myGO.transform;
          BuilderPanel.name = "PanelBuilder";
          BuilderPanel.AddComponent<CanvasRenderer>();
          BuilderPanel.AddComponent<RectTransform>();
          BuilderPanel.AddComponent<Image>();
          BuilderPanel.GetComponent<Image>().sprite = UIbackgroud;
          BuilderPanel.GetComponent<Image>().color = new Color (0,0,0,0.5f);
          BuilderPanel.GetComponent<RectTransform>().anchorMax = new Vector2(0.33f, 0.33f);
          BuilderPanel.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
          BuilderPanel.GetComponent<RectTransform>().pivot = new Vector2(0f, 0f);
          BuilderPanel.GetComponent<RectTransform>().position = new Vector3(0f, 0f, 0f);

          BuilderPanel.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
          */

        /*
        GameObject MiddlePanel = new GameObject();
        MiddlePanel.transform.parent = myGO.transform;
        MiddlePanel.name = "Panel";
        MiddlePanel.AddComponent<CanvasRenderer>();
        MiddlePanel.AddComponent<RectTransform>();
        MiddlePanel.AddComponent<Image>();
        MiddlePanel.GetComponent<Image>().sprite = UIbackgroud;
        MiddlePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.8f);
        MiddlePanel.GetComponent<RectTransform>().anchorMax = new Vector2(0.66f, 0.33f);
        MiddlePanel.GetComponent<RectTransform>().anchorMin = new Vector2(0.33f, 0);
        MiddlePanel.GetComponent<RectTransform>().pivot = new Vector2(0f, 0f);
        MiddlePanel.GetComponent<RectTransform>().position = new Vector3(0f, 0f, 0f);
        MiddlePanel.GetComponent<RectTransform>().right = new Vector3(0f, 0f, 0f);
        MiddlePanel.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        MiddlePanel.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);

        GameObject RightPanel = new GameObject();
        RightPanel.transform.parent = myGO.transform;
        RightPanel.name = "Panel";
        RightPanel.AddComponent<CanvasRenderer>();
        RightPanel.AddComponent<RectTransform>();
        RightPanel.AddComponent<Image>();
        RightPanel.GetComponent<Image>().sprite = UIbackgroud;
        RightPanel.GetComponent<Image>().color = new Color(0, 0, 0, 1f);
        RightPanel.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 0.33f);
        RightPanel.GetComponent<RectTransform>().anchorMin = new Vector2(0.66f, 0);
        RightPanel.GetComponent<RectTransform>().pivot = new Vector2(0f, 0f);
        RightPanel.GetComponent<RectTransform>().position = new Vector3(0f, 0f, 0f);
        RightPanel.GetComponent<RectTransform>().right = new Vector3(0f, 0f, 0f);
        RightPanel.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        RightPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
        */
        

        
        //button 
        GameObject myButton = new GameObject();
        myButton.transform.parent = BuilderPanel.transform;
        myButton.name = "Building";
        myButton.AddComponent<RectTransform>();
        myButton.AddComponent<Button>();
        myButton.AddComponent<Image>();
        myButton.GetComponent<Button>().targetGraphic = myButton.GetComponent<Image>();
        myButton.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        myButton.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
        myButton.GetComponent<RectTransform>().pivot = new Vector2(0f, 0f);
        myButton.GetComponent<RectTransform>().position = new Vector3(0f, 0f, 0f);
        myButton.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        myButton.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
        myButton.GetComponent<Button>().onClick.AddListener(() => {Build.SetBuilding(myButton.name) ; });

        CreateText(TextBuilderButton,
                    myButton,
                    "Main Building  " +
                    "5KW " +
                    "3KG",
                    ArialFont,
                    new Vector2(1f, 1f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f)
                    );
        TextBuilderButton.GetComponent<Text>().fontSize = 18;

        createButton(StorageButton,
                        BuilderPanel,
                        "Storage",
                        new Vector3(0f, 0f, 0f),
                        new Vector2(1f, 1f),
                        new Vector2(0.5f, 0.5f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f));

        StorageButton.GetComponent<Button>().onClick.AddListener(() => { Build.SetBuilding(StorageButton.name); });

        CreateText(TextStorageButton,
                    StorageButton,
                    "Storage" +
                    " 200W " +
                    "100G",
                    ArialFont,
                    new Vector2(1f, 1f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f)
                    );

        createButton(HospitalButton,
                        BuilderPanel,
                        "Hospital",
                        new Vector3(0f, 0f, 0f),
                        new Vector2(0.5f, 1f),
                        new Vector2(0f, 0.5f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f));

        HospitalButton.GetComponent<Button>().onClick.AddListener(() => { Build.SetBuilding(HospitalButton.name); });

        CreateText(TextHospitalButton,
                    HospitalButton,
                    "Hospital" +
                    " 300W " +
                    "300G",
                    ArialFont,
                    new Vector2(1f, 1f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f)
                    );

        createButton(BarracsButton,
                        BuilderPanel,
                        "Barracks",
                        new Vector3(0f, 0f, 0f),
                        new Vector2(1f, 0.5f),
                        new Vector2(0.5f, 0f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f),
                        new Vector2(0f, 0f));

        BarracsButton.GetComponent<Button>().onClick.AddListener(() => { Build.SetBuilding(BarracsButton.name); });

        CreateText(TextBarracsButton,
                    BarracsButton,
                    "Barracks" +
                    " 400W " +
                    "200G",
                    ArialFont,
                    new Vector2(1f, 1f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f)
                    );

        CreateText(SoldierText,
                    SoldierPanel,
                    "HP:",
                    ArialFont,
                    new Vector2(1f, 1f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f)
                    );

        CreateText(WarriroText,
                    WarriorPanel,
                    "HP: ",
                    ArialFont,
                    new Vector2(1f, 1f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f)
                    );

        CreateText(RangeUnitText,
                    RangeUnitPanel,
                    "HP: ",
                    ArialFont,
                    new Vector2(1f, 1f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f),
                    new Vector2(0f, 0f)
                    );


        BuilderPanel.SetActive(false);

        SoldierText.GetComponent<Text>().color = Color.white;
        WarriroText.GetComponent<Text>().color = Color.white;
        RangeUnitText.GetComponent<Text>().color = Color.white;
    }

    



    // Update is called once per frame
    void Update()
    {
        if (GetComponent<GameControls>().selectedUnits.Count > 0)
        {
            SoldierText.GetComponent<Text>().text = "HP:   " + GetComponent<GameControls>().selectedUnits[0].GetComponent<Unit>().Health.ToString() + 
                                     "              Damage:  " + GetComponent<GameControls>().selectedUnits[0].GetComponent<Unit>().Damage.ToString();

            WarriroText.GetComponent<Text>().text = "HP:   " + GetComponent<GameControls>().selectedUnits[0].GetComponent<Unit>().Health.ToString() +
                                      "             Damage:  " + GetComponent<GameControls>().selectedUnits[0].GetComponent<Unit>().Damage.ToString();

            RangeUnitText.GetComponent<Text>().text = "HP:   " + GetComponent<GameControls>().selectedUnits[0].GetComponent<Unit>().Health.ToString() +
                                       "             Damage:  " + GetComponent<GameControls>().selectedUnits[0].GetComponent<Unit>().Damage.ToString();
        }
        
    }


    
}
