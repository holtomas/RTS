using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickObject : MonoBehaviour
{
    // promene
    public bool selected = false;
    NavMeshAgent agent;

    Camera cam;
    GameControls gameControls;
    public LayerMask groudLayer;


    private void Awake()
    {
        cam = Camera.main;

    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent> ();
     //   GameObject ob = GameObject.Find("GameControl");
       // gameControls = ob.GetComponent<GameControls>();
    }

   

    public void SetPath()
    {

    }

}
