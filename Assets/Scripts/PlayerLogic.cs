using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class PlayerLogic : MonoBehaviour
{
    public bool[] keys = { false, false, false };
    //public bool bossKey = false;

    public int health = 100;
    private int maxHealth = 100;
    public bool invulnerable = false;

    public int money;

    public bool secondWeapon;

    public int currentLevel;

    public GameObject GameLogicMg;

    public GameObject chest;
    private bool chestNearby, chestOpened;

    public GameObject lever;
    private bool leverNearby, leverPulled;

    public GameObject door;
    private GameObject doorRight, doorLeft;
    public GameObject switchObj;

    public GameObject doorHint;

    public GameObject barrel;
    public EnemyController enemyController;

    public GameObject sword;


    //temp for checking room triggers
    private bool triggerKitchen, triggerStorage, triggerHallway, triggerDinningHall;
   

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        GameLogicMg = GameObject.Find("GameLogicManager");
        door = GameObject.Find("Door");
        if(currentLevel == 2)
        {
            doorRight = GameObject.Find("DoorRight");
            doorLeft = GameObject.Find("DoorLeft");
        }
        getStats();
    }

    // Update is called once per frame
    void Update()
    {
        //Death
        if (health <= 0)
        {
            setStats();
            //set death animation?
            GameLogicMg.GetComponent<GameLogicManager>().LooseGame();
        }

        checkSpecialKeys();

        if (currentLevel != 3)
        {
            checkChest();
            checkDoor();
        }

        if (currentLevel == 0) checkLever();

        //Temp for pushing switch on level 2, replace with arrow hit
        if (Input.GetKey("f") && currentLevel == 1) switchObj.GetComponent<SwitchLogic>().push();

        //temp
        if (currentLevel == 2 && Input.GetKey("f"))
        {
            Destroy(barrel);
            chest.GetComponent<ChestLogic>().unlock();
        }

    }

    private void checkDoor()
    {
        //Use Door
        float distDoor = Vector3.Distance(transform.position, door.transform.position);
        if (distDoor < 15 && Input.GetKey("e")) UseDoor();
    }

    private void checkLever()
    {
        //Use lever
        float distLever = Vector3.Distance(lever.transform.position, transform.position);
        if (distLever < 4)
        {
            leverNearby = true;
            if (Input.GetKey("e")) pullLever();
        }
    }

    //If chest is close enough -> give option to open
    private void checkChest()
    {
        float distChest = Vector3.Distance(transform.position, chest.transform.position);
        if (distChest < 2)
        {
            Debug.Log("Close to chest");
            chestNearby = true;
            if (Input.GetKey("e")) openChest();
        }
    }

    private void checkSpecialKeys()
    {
        if (Input.GetKey("k")) keys[currentLevel] = true;
        if (Input.GetKey("b")) keys[2] = true;
        if (Input.GetKey("g")) invulnerable = true;
    }

    //Update stats from singleton class between scenes
    private void getStats()
    {
        keys = PlayerStats.Instance.keys;
        this.GetComponent<MainPlayerLogics>().health = PlayerStats.Instance.health;
        this.GetComponent<MainPlayerLogics>().money = PlayerStats.Instance.money;
        this.GetComponent<MainPlayerLogics>().HasCrossBow = PlayerStats.Instance.hasCrossbow;
        this.GetComponent<MainPlayerLogics>().activeSword = PlayerStats.Instance.swordActive;
        this.GetComponent<MainPlayerLogics>().invulnerable = PlayerStats.Instance.invulnerable;
    }

    public void setStats()
    {
        PlayerStats.Instance.keys = keys;
        PlayerStats.Instance.health = this.GetComponent<MainPlayerLogics>().health;
        PlayerStats.Instance.money = this.GetComponent<MainPlayerLogics>().money;
        PlayerStats.Instance.hasCrossbow = this.GetComponent<MainPlayerLogics>().HasCrossBow;
        PlayerStats.Instance.swordActive = this.GetComponent<MainPlayerLogics>().activeSword;
        PlayerStats.Instance.invulnerable = this.GetComponent<MainPlayerLogics>().invulnerable;
    }

    public void UseDoor()
    {
        bool hasKey = false;
        switch (door.tag)
        {
            case "Door1":
                hasKey = keys[0];
                break;
            case "Door2":
                hasKey = keys[1];
                break;
            case "Door3":
                hasKey = keys[2];
                break;
            default:
                break;
        }
        if (hasKey)
        {
            doorHint.SetActive(false);
            if (currentLevel != 2)
            {
                DoorLogic doorScript = door.GetComponent<DoorLogic>();
                if (doorScript.IsOpen) doorScript.Close();
                else doorScript.Open(transform.position);
            }
            else
            {
                DoorLogic doorRightScript = doorRight.GetComponent<DoorLogic>();
                DoorLogic doorLeftScript = doorLeft.GetComponent<DoorLogic>();
                if(doorLeftScript.IsOpen)
                {
                    doorLeftScript.Close();
                    doorRightScript.Close();
                }
                else
                {
                    doorLeftScript.Open(transform.position);
                    doorRightScript.Open(transform.position);
                }

            } 
        }
        else
        {
            doorHint.SetActive(true);
        }
    }

    private void pullLever()
    {
        leverPulled = true;
        lever.GetComponent<LeverScript>().PullLever();
    }

    private void openChest()
    {
        chest.GetComponent<ChestLogic>().open();      
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "HealthPotion":
                int newhealth = health + 10;
                if (newhealth > maxHealth) health = maxHealth;
                else health = newhealth;
                Destroy(other.gameObject);
                break;
            case "Key1":
                keys[0] = true;
                Destroy(other.gameObject);
                break;
            case "Key2":
                keys[1] = true;
                Destroy(other.gameObject);
                break;
            case "Key3":
                keys[2] = true;
                Destroy(other.gameObject);
                break;
            case "TriggerDoor":
                manageDoorTriggers(other);
                break;
            case "EndScene1":
                if (true)
                {
                    setStats();
                    GameLogicMg.GetComponent<GameLogicManager>().LoadScene2();
                }                
                break;
            case "EndScene2":
                setStats();
                GameLogicMg.GetComponent<GameLogicManager>().LoadScene3();
                break;
            case "EndScene3":
                setStats();
                GameLogicMg.GetComponent<GameLogicManager>().LoadSceneBoss();
                break;
            case "Money":
                money++;
                break;
            case "DieFall":
                health = 0;
                GameLogicMg.GetComponent<GameLogicManager>().LooseGame();
                break;
            default:
                break;
        }
    }

    private bool checkSwordCollision(Collider other)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            if (col.bounds.Intersects(other.bounds) && col.gameObject == sword)
            {
                return true;
            }
        }
        return false;
    }

    private void manageDoorTriggers(Collider other)
    {
        Vector3 dir = other.transform.position - transform.position;
        dir.Normalize();
        switch (other.gameObject.name)
        {
            //LEVEL1
            case "TriggerKitchen1":
                if (dir.z > 0) enemyController.ActivateEnemiesRoom(2); //temp: call func to generate kitchen enemies
                else enemyController.ActivateEnemiesRoom(1); //call func to generate storage enemies if necessary
                break;
            case "TriggerKitchen2":
                if (dir.x < 0) enemyController.ActivateEnemiesRoom(2); //call func to gen kitchen enemies
                else enemyController.ActivateEnemiesRoom(3);  //call func to gen hallway enemies?
                break;
            case "TriggerHall":
                if (dir.x > 0) enemyController.ActivateEnemiesRoom(4); //call func to gen dining hall enemies
                else enemyController.ActivateEnemiesRoom(3); //call func to gen hallway enemies?
                break;
            //LEVEL2
            case "TriggerDoor1":
                if (dir.x > 0) enemyController.ActivateEnemiesRoom(2);
                else{
                    enemyController.ActivateEnemiesRoom(1);
                    Debug.Log("EnterRoom1");
                } 
                break;
            case "TriggerDoor2":
                if (dir.z > 0) enemyController.ActivateEnemiesRoom(2);
                else enemyController.ActivateEnemiesRoom(3);
                break;
            //LEVEL3
            case "TriggerRoom1":
                if (dir.z < 0) enemyController.ActivateEnemiesRoom(2);
                else Debug.Log("EnterHallway");
                break;
            case "TriggerRoom2":
                if (dir.z > 0) Debug.Log("EnterRoom2");
                else Debug.Log("EnterHallway");
                break;
            case "TriggerRoom3":
                if (dir.z < 0) enemyController.ActivateEnemiesRoom(3);
                else Debug.Log("EnterHallway");
                break;
            case "TriggerRoom4":
                if (dir.z > 0) enemyController.ActivateEnemiesRoom(4);
                else Debug.Log("EnterHallway");
                break;
            default:
                break;
        }
        
    }
}
