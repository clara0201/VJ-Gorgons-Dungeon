using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GorgonBehaviour : MonoBehaviour
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
    public GameObject eye1;
    public GameObject eye2;
    
    public GameObject target; 
    public MainPlayerLogics mainPlayerLogics;

    public bool attacking;
    public NavMeshAgent agent;
    public float distanceAtack;
    public float visionRadius;
    public int eyesAlive;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mainPlayerLogics = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerLogics>();
        agent.enabled = true;
        health = 100;
        eyesAlive = 2;
    }    
    public void Gorgon_Behaviour(){
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
    }
    public void Hit(){
        anim.Play("Hit");
        //AUDIO DE HIT
        health = health - 10;         
    }

    public void Die(){

        gameObject.SetActive(false);   
        GameObject.Find("GameLogicManager").GetComponent<GameLogicManager>().WinGame();
    }
    public void Reappear(){
        health = 100;
        gameObject.SetActive(true);
    }
    public void EyeHit(){
        Debug.Log("eye hit");
        anim.Play("Hit");
        if(eyesAlive == 2) Destroy(eye1);
        else if(eyesAlive == 1) Destroy(eye2);
        eyesAlive--;
        health = health - 30;
    }
    void Update()
    {
        Gorgon_Behaviour();
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Gorgon_Attack_01" && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >=  0.55f) && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <  0.56f))
        {
            //audio de puñetazo
            mainPlayerLogics.Hit(30);
            EndAnimation();
        }
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Gorgon_Hit" && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >=  0.65f) && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <  0.67f) && health <= 0 && eyesAlive == 0)
        {
            //audio de morir
            Die();
        }
    }

    }
    // Update is called once per frame
