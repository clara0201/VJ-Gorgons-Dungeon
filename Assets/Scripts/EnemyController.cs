using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject[] enemiesRoom1; 
    public GameObject[] enemiesRoom2; 
    public GameObject[] enemiesRoom3; 
    public GameObject[] enemiesRoom4;  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public void ActivateEnemiesRoom(int roomNum){
        GameObject[] enemies = new GameObject[10]; 
        switch(roomNum){
            case 1: enemies = enemiesRoom1;
                break;
            case 2: enemies = enemiesRoom2;
                break;
            case 3: enemies = enemiesRoom3;
                break;
            case 4: enemies = enemiesRoom4;
                break;
        
        }
        for (int i= 0; i < enemies.Length; i++){
            if(enemies[i].tag == "Spider") enemies[i].GetComponent<EnemyBehaviour>().Reappear();
            if(enemies[i].tag == "Minotaur") enemies[i].GetComponent<MinotaurBehaviour>().Reappear();
            if(enemies[i].tag == "Satyr") enemies[i].GetComponent<SatyrBehaviour>().Reappear();
            if(enemies[i].tag == "Harpy") enemies[i].GetComponent<HarpyBehaviour>().Reappear(); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        //  if(Input.GetKeyDown(KeyCode.B)){
        //     ActivateEnemiesRoom(1);
        // }
        // if(Input.GetKeyDown(KeyCode.C)){
        //     ActivateEnemiesRoom(2);
        // }
    }
}
