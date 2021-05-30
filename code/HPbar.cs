using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class HPbar : MonoBehaviour
{


    public Transform cam;
    public Slider slider;
    public Unit unit;
    public Building building;
    

    // Start is called before the first frame update
    void Start()
    {

        
        slider = GetComponentInChildren<Slider>();
         


       

        

        if (GetComponentInParent<Unit>() != null)
        {
            unit =  GetComponentInParent<Unit>();
            slider.maxValue = unit.Health;
            GameObject pl1 = GameObject.Find("Player");
            cam = pl1.transform;
        }
        else
        {
            building = GetComponentInParent<Building>();
            GameObject pl1 = GameObject.Find("Player");
            cam = pl1.transform;

            slider.maxValue = building.health;
            building.health = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (building == null)
        {
            slider.value = unit.Health;
        }
        else
        {
            slider.value = building.health;
        }

    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }

    


}
