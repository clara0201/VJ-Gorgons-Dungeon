using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapons : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] weapons;
    public int numSwords = 0;
    public MainPlayerLogics mainPlayerLogics;

    void Start()
    {

        mainPlayerLogics = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerLogics>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ActivateWeapons(int number, string weaponType){
        if(weaponType == "Sword")numSwords++;

        for (int i= 0; i < weapons.Length; i++){
            if(weaponType != "Crossbow"){
                if(weapons[i].tag == "Crossbow"){
                    weapons[i].SetActive(false);
                    mainPlayerLogics.hasCrossBow(false);
                    
                }
            }
            if(weaponType == "Shield"){
                if(weapons[i].tag =="LeftSword") weapons[i].SetActive(false);
                
            }
            else if(weaponType == "Crossbow"){
                numSwords = 0;
                 weapons[i].SetActive(false);
                mainPlayerLogics.activeSword = false;
            }
            else if(weaponType == "Sword" ){
                Debug.Log("get sword tag: " + weapons[i].tag + " num swords: "+ numSwords);
                    if(weapons[i].tag == "Shield" && numSwords == 2) {
                        Debug.Log("no more shield");
                        weapons[i].SetActive(false);   
                        mainPlayerLogics.hasShield(false);        
                    }
                }
            }
        weapons[number].SetActive(true);
        if(weaponType == "Shield") mainPlayerLogics.hasShield(true);
        if(weaponType == "Crossbow") {
            mainPlayerLogics.hasCrossBow(true);        
            mainPlayerLogics.hasShield(false);
        }
        }
    }
    
