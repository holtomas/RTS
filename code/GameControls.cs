using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GameControls : MonoBehaviour
{
    public UI ui;
    GameObject SelUnit;
    RaycastHit hit;
    public List<Transform> selectedUnits = new List<Transform>();
    private List<Transform> Buildings = new List<Transform>();
    public List<Transform> EnemyBuildings = new List<Transform>();
    public GameObject SelectedBuilding;
    bool isDragging = false;
    Vector3 mousePosition;
    public float team;

    public ResUI resUI;

    void Awake()
    {
        ui = GetComponent<UI>();
        resUI = GetComponent<ResUI>();
    }
    private void Start()
    {
        foreach (var item in FindObjectsOfType<Building>())
        {
            EnemyBuildings.Add(item.transform);
        }


    }

    private void OnGUI()
    {
        if (isDragging)
        {
            Rect rect = ScreenHelper.GetScreenRect(mousePosition, Input.mousePosition);
            ScreenHelper.DrawScreenReact(rect, new Color(0.8f, 0.8f, 0.95f, 0.1f));
            ScreenHelper.DrawScreenRectBorder(rect, 1, Color.blue);
        }


    }






    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<BuildBuilding>().activeBuilding == false)
        {
            mousePosition = Input.mousePosition;

            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!IsPointerOverUIObject())
            {
                if (Physics.Raycast(camRay, out hit, 5000f, -1, QueryTriggerInteraction.Ignore))
                {

                    if (!hit.transform.CompareTag("Unit"))
                    {
                        if (selectedUnits.Count == 0)
                        {
                            DeselectUnits();
                        }
                        else
                        {
                            ui.Panels[selectedUnits[0].GetComponent<Unit>().UnitType + "Panel"].SetActive(false);
                            DeselectUnits();
                        }
                    }

                    if (hit.transform.CompareTag("Building") && hit.transform.GetComponent<Building>().team == 1)
                    {
                        if (selectedUnits.Count == 0)
                        {
                            DeselectUnits();
                        }
                        else
                        {
                            ui.Panels[selectedUnits[0].GetComponent<Unit>().UnitType + "Panel"].SetActive(false);
                            DeselectUnits();
                        }


                        hit.transform.GetComponent<Building>().Selected = true;
                        SelectedBuilding = hit.transform.gameObject;

                        if (SelectedBuilding.GetComponent<Building>().HavePanel)
                        {

                            ui.Panels[SelectedBuilding.GetComponent<Building>().Panel].SetActive(true);
                        }
                    }
                    else
                    {
                        if (SelectedBuilding != null)
                        {

                            DeselectBuilding(SelectedBuilding.transform);

                        }
                    }

                    if (hit.transform.CompareTag("Resource"))
                    {
                        Debug.Log("kliknuti na surovinu");
                    }




                    if (hit.transform.CompareTag("Unit") && (selectedUnits.Count == 0 || Input.GetKey(KeyCode.LeftShift)) && hit.transform.GetComponent<Unit>().Team == 1)
                    {
                        if (SelectedBuilding != null)
                        {

                            SelectedBuilding.GetComponent<Building>().Selected = false;
                        }
                        SelectUnit(hit.transform, true);
                        ui.Panels[selectedUnits[0].GetComponent<Unit>().UnitType + "Panel"].SetActive(true);
                    }

                    else if (hit.transform.CompareTag("Unit") && hit.transform.GetComponent<Unit>().Team == 1)
                    {
                        if (SelectedBuilding != null)
                        {

                            SelectedBuilding.GetComponent<Building>().Selected = false;
                        }

                        if (selectedUnits.Count == 0)
                        {
                            SelectUnit(hit.transform, true);
                        }
                        else
                        {
                            ui.Panels[selectedUnits[0].GetComponent<Unit>().UnitType + "Panel"].SetActive(false);
                            DeselectUnits();
                            SelectUnit(hit.transform, true);
                        }


                    }


                    else
                    {

                        isDragging = true;


                    }

                }
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging != false)
            {
                if (selectedUnits.Count != 0)
                {
                    ui.Panels[selectedUnits[0].GetComponent<Unit>().UnitType + "Panel"].SetActive(false);
                }
                DeselectUnits();

            }



            foreach (var selectableObject in FindObjectsOfType<ClickObject>())
            {
                if (isWithinSelectBounds(selectableObject.transform) && selectableObject.GetComponent<Unit>().Team == 1)
                {

                    selectableObject.GetComponent<ClickObject>().selected = true;
                    SelectUnit(selectableObject.transform, true);

                }
            }

            isDragging = false;

        }
    }


    private void SelectUnit(Transform unit, bool isMultiSelect = false)
    {
        if (!isMultiSelect)
        {
            DeselectUnits();
        }

        selectedUnits.Add(unit);
        ui.Panels[selectedUnits[0].GetComponent<Unit>().UnitType + "Panel"].SetActive(true);
        unit.GetComponent<ClickObject>().selected = true;

    }

    private void DeselectUnits()
    {
        foreach (var unit in selectedUnits)
        {
            unit.GetComponent<ClickObject>().selected = false;
        }

        selectedUnits.Clear();
    }

    private bool isWithinSelectBounds(Transform transform)
    {
        if (!isDragging)
        {
            return false;
        }


        Camera camera = Camera.main;
        Bounds viewportBonds = ScreenHelper.GetViewportBounds(camera, mousePosition, Input.mousePosition);
        return viewportBonds.Contains(camera.WorldToViewportPoint(transform.position));


    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void DeselectBuilding(Transform building)
    {
        building.GetComponent<Building>().Selected = false;

        if (SelectedBuilding.GetComponent<Building>().HavePanel)
        {

            ui.Panels[SelectedBuilding.GetComponent<Building>().Panel].SetActive(false);
        }

        SelectedBuilding = null;

    }

}
