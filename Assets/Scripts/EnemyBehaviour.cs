using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public int routine; 
    public float chronometer;
    public Animator anim;
    public Quaternion angle;
    public float degree;
    public int  speed;
    public int health;

    public GameObject prefabBag;
    public GameObject prefabPotion;
    
    public GameObject target; 
    public MainPlayerLogics mainPlayerLogics;

    public bool attacking = false;
    public bool canAttack = true;
    public bool hasBeenKilled = false;

    public NavMeshAgent agent;
    public float distanceAtack;
    public float visionRadius;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mainPlayerLogics = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerLogics>();
        agent.enabled = true;
        health = 10;
    }
    void Update()
    {
        Enemy_Behaviour();
        if (anim.GetCurrentAnimatorClipInfo(0).Length > 0 && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "HalfSpider_Attack_02")
        {
            //audio de atack de araña
        
            if ((anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.85f) && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.87f) && canAttack)
            {
                mainPlayerLogics.Hit(10);
                canAttack = false;
            }

            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) EndAnimation();
        }
        if (anim.GetCurrentAnimatorClipInfo(0).Length > 0 && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "HalfSpider_Hit" && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.85f) && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.87f) && health <= 0)
        // audio de morir
        {
            Die();
        }
    }

    public void Enemy_Behaviour(){
        if(Vector3.Distance(transform.position, target.transform.position) > visionRadius){
            anim.SetBool("Run", false);
            chronometer += 1*Time.deltaTime;
            if(chronometer >= 4){
                routine = Random.Range(0,2);
                chronometer = 0;
            }
            switch(routine){
                case 0: //idle
                    anim.SetBool("Walk", false);
                    break;
                case 1: //direccion de desplazmiento
                    degree = Random.Range(0,360);
                    angle = Quaternion.Euler(0, degree, 0);
                    routine++;
                    break;
                case 2: //caminar hacia adelante
                    anim.SetBool("Walk", true);                    
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    break;
            }
        }
        else{
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);                           
            agent.enabled = true;            
            agent.SetDestination(target.transform.position);
            if(Vector3.Distance(transform.position, target.transform.position) > distanceAtack && !attacking){
                anim.SetBool("Walk", false);
                anim.SetBool("Run", true);
            }
            else{
                if(!attacking){
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 1);
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", false);

                    anim.SetBool("Attack", true);
                    attacking = true;
                }
            }
        }
        if(attacking){
            agent.enabled = false;
        }
    }

    public void EndAnimation(){
        anim.SetBool("Attack", false);
        attacking = false;
        canAttack = true;
    }

    public void Hit(){
        anim.Play("Hit");
        //AUDIO DE HIT
        health = health - 10;    
    }

    public void Die(){
        gameObject.SetActive(false);

        if (Random.Range(0,2) == 0) {
            Instantiate(prefabBag,transform.position, transform.rotation);
        }
        else {
            Instantiate(prefabPotion,transform.position, transform.rotation);
        }
        this.hasBeenKilled = true;
    }

    public void Reappear(){
        if (!hasBeenKilled) {
            health = 10;
            gameObject.SetActive(true);
            agent.enabled = true;
            attacking = false;
        }    
    }
    

}
