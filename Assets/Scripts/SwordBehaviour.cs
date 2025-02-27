using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    public MainPlayerLogics mainPlayerLogics;

    // Start is called before the first frame update
    void Start()
    {
        mainPlayerLogics = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerLogics>();
        
    }
    
     void OnTriggerEnter(Collider collision){       
        bool isAttacking = mainPlayerLogics.isAttacking(); 
        if(collision.gameObject.tag == "Spider" && isAttacking == true){            
            collision.gameObject.GetComponent<EnemyBehaviour>().Hit();            
            }
        if(collision.gameObject.tag == "Minotaur" && isAttacking == true){            
        collision.gameObject.GetComponent<MinotaurBehaviour>().Hit();            
        }
        if(collision.gameObject.tag == "Harpy" && isAttacking == true){            
        collision.gameObject.GetComponent<HarpyBehaviour>().Hit();            
        }
        if(collision.gameObject.tag == "Satyr" && isAttacking == true){            
        collision.gameObject.GetComponent<SatyrBehaviour>().Hit();            
        }
        if(collision.gameObject.tag == "Gorgon" && isAttacking == true){               
            collision.gameObject.GetComponent<GorgonBehaviour>().Hit();            
        }
        if(collision.gameObject.tag == "Barrel" && isAttacking == true)
        {
            //add particle sistem
            Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {   int PlayerHealth = mainPlayerLogics.getHealth();
        if( PlayerHealth >= 0 && PlayerHealth <= 60) transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        else if( PlayerHealth > 60 && PlayerHealth <= 70) transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        else if(PlayerHealth >=  80) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
