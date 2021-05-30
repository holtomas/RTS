using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Unit : MonoBehaviour
{
    public float Health;
    public float Range;
    public float Damage;
    public string UnitType;
    public string PanelName;
    public bool RepairActive = false;
    public bool Build = false;
    public int Team;
    public bool moving = false;
    public bool mining = false;
    public float Maxhealth;
    public bool Attacking = false;
    public GameObject Enemy;
    public bool Following = false;

    public  Vector3 MovingTo;

    public bool EnemyInRange = false;

    private List<Unit> Enemies = new List<Unit>();


    public float AttackperSec = 1f;
    public float MineperSec = 1f;

    public float WoodCost;
    public float GoldCost;


    public float Backpack = 0;
    public float GoldBack = 0;
    public float WoodBack = 0;
    public float BackpackSize = 10;

    public Vector3 MinePoint;
    public GameObject StoragePoint;

    public GameObject selectedBuilding;
    private Vector3 curPos = new Vector3(0f, 0f, 0f);
    private Vector3 lastPos = new Vector3(0f, 0f, 0f);

    public Material team1;
    public Material team2;

    GameObject player;

    public bool FindEnemy = false;

    public float prerange;







    // Start is called before the first frame update
    void Start()
    {
        MovingTo = transform.position;
        prerange = Range;
        player = GameObject.Find("GameControl");
        if (Team == 1)
        {
            transform.gameObject.GetComponent<MeshRenderer>().material = team1;
        }
        else
        {
            transform.gameObject.GetComponent<MeshRenderer>().material = team2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            if (player.GetComponent<GameControls>().selectedUnits.Contains(this.transform))
            {

                if (player.GetComponent<GameControls>().selectedUnits.Count > 1)
                {
                    player.GetComponent<UI>().Panels[player.GetComponent<GameControls>().selectedUnits[0].GetComponent<Unit>().UnitType + "Panel"].SetActive(true);

                }
                else
                {
                    player.GetComponent<UI>().Panels[player.GetComponent<GameControls>().selectedUnits[0].GetComponent<Unit>().UnitType + "Panel"].SetActive(false);
                }

                player.GetComponent<GameControls>().selectedUnits.Remove(this.transform);
            }

            Destroy(this.gameObject);
        }



        curPos = transform.position;
        if (curPos == lastPos)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
        lastPos = curPos;
        
        if (UnitType != "Builder")
        {
            if (Enemy != null)
            {

                

                if (Vector3.Distance(Enemy.transform.position, this.transform.position) < Range && !EnemyInRange && Attacking)
                {
                    GetComponent<NavMeshAgent>().SetDestination(transform.position);
                    EnemyInRange = true;
                    GetComponent<NavMeshAgent>().enabled = false;
                    GetComponent<NavMeshObstacle>().enabled = true;
                    StartCoroutine(Attack(AttackperSec, Enemy));

                }

                else if (!moving && Attacking && !EnemyInRange && FindEnemy)
                {
                    GetComponent<NavMeshObstacle>().enabled = false;
                    GetComponent<NavMeshAgent>().enabled = true;

                    GetComponent<NavMeshAgent>().SetDestination(Enemy.transform.position);
                    FindEnemy = false;
                }

                else if (!Following && !EnemyInRange)
                {
                    StartCoroutine(Follow(Enemy));
                }

            }

          
            if (Enemy == null && Enemies.Count > 0)
            {
                foreach (var unit in Enemies)
                {
                    if (unit == null)
                    {
                        Enemies.Remove(unit);
                        break;
                    }
                    if (Enemy == null)
                    {
                        Enemy = unit.transform.gameObject;
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, Enemy.transform.position) > Vector3.Distance(transform.position, unit.transform.position))
                        {
                            Enemy = unit.transform.gameObject;
                        }
                    }


                }

                if (Enemy != null)
                {
                    FindEnemy = true;
                }

            }
        
        
        }
        



    }


    

    private void OnTriggerEnter(Collider unit)
    {
        if (unit.GetComponent<Unit>() != null && unit.GetComponent<Unit>().Team != Team && UnitType != "Builder" && !unit.isTrigger)
        {
            Enemies.Add(unit.GetComponent<Unit>());
            if (!moving && Enemy == null)
            {
                GetComponent<NavMeshObstacle>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = true;
                GetComponent<NavMeshAgent>().SetDestination(unit.transform.position);
                Enemy = unit.transform.gameObject;
                Attacking = true;

            }
        }

       else if (unit.GetComponent<Building>() != null && unit.GetComponent<Building>().team != Team && UnitType != "Builder" && !unit.isTrigger)
        {
            Enemies.Add(unit.GetComponent<Unit>());
            if (!moving && Enemy == null)
            {
                GetComponent<NavMeshObstacle>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = true;
                GetComponent<NavMeshAgent>().SetDestination(unit.transform.position);
                Enemy = unit.transform.gameObject;
                Attacking = true;
                

            }
        }
    }

    private void OnTriggerExit(Collider unit)
    {
        if (Enemies.Contains(unit.GetComponent<Unit>()))
        {
            Enemies.Remove(unit.GetComponent<Unit>());
            Enemy = null;

        }
    }
    
    public IEnumerator Attack(float speed, GameObject Enemy)
    {

        yield return new WaitForSeconds(AttackperSec);
        if (Enemy == null)
        {
            EnemyInRange = false;
            GetComponent<NavMeshObstacle>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = true;

        }
        else if (Vector3.Distance(Enemy.transform.position, this.transform.position) < Range)
        {
            if (Enemy.GetComponent<Unit>() != null)
            {
                Enemy.GetComponent<Unit>().Health -= Damage;
                StartCoroutine(Attack(speed, Enemy));
            }

            else
            {
                Enemy.GetComponent<Building>().health -= Damage;
                StartCoroutine(Attack(speed, Enemy));
            }
            
        }
        else
        {
            EnemyInRange = false;
            GetComponent<NavMeshObstacle>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = true;

        }

    }
    
    public IEnumerator Follow(GameObject Enemy)
    {
        yield return new WaitForSeconds(0.5f);
        if (!moving)
        {
            if (GetComponent<NavMeshAgent>().enabled && Enemy != null)
            {
                GetComponent<NavMeshAgent>().SetDestination(Enemy.transform.position);
                
            }
            Following = false;
        }
        
    }
    
    public IEnumerator Folllow(GameObject enemy)
    {   
        GetComponent<NavMeshAgent>().SetDestination(enemy.transform.position);
        yield return new WaitForSeconds(1f);
        if (!EnemyInRange && Attacking && Enemy == enemy)
        {
            Folllow(enemy);
        }
        else
        {
            Following = false;
        }
    }
    
}