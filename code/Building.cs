using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    public float health;
    public float healthMax;
    public int team;
    public string BuildingType;
    public string PanelName;
    public float Range;
    public bool Completed = false;
    public bool Selected = false;
    public Vector3 RallyPoint;
    public GameObject rallyPoint;
    public Vector3 SpawnPoint;
    public GameObject Spawn;
    public bool HavePanel = false;
    public string Panel;
    public bool Recruiting = false;
    public float Clicks = 0;
    public float WoodCost;
    public float GoldCost;
    public Material team1;
    public Material team2;

   public GameControls gm;






    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameControls>();

        if (team == 1)
        {
            transform.gameObject.GetComponent<MeshRenderer>().material = team1;
        }
        else
        {
            health = healthMax;
            GetComponent<Collider>().enabled = true;
            transform.gameObject.GetComponent<MeshRenderer>().material = team2;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            gm.EnemyBuildings.Remove(this.gameObject.transform);
            if (gm.EnemyBuildings.Count == 0)
            {
                
            }
           
            Destroy(this.gameObject);
        }
    }
}
