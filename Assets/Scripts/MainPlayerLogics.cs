using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerLogics : MonoBehaviour
{
    public float speed = 8.0f;
    public float rotationSpeed = 100.0f;
    public float JumpForce = 50.0f;
    public int health = 100; //max 100
    public int money;
    public bool invulnerable;

    private Animator anim;
    private Rigidbody Physics;
    public bool IsGrounded = true;
    public bool HasShield;
    public bool HasCrossBow;    
    public bool Defense = false;
    public GameObject prefabArrow;
    public GameObject prefabSpider;
    public GameObject crossBow;
    public GameObject sword;
    public GameObject gameLogicManager;
    public bool activeSword;

    public float x, y;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Physics = GetComponent<Rigidbody>();
        gameLogicManager = GameObject.Find("GameLogicManager");
        HasCrossBow = false;
        invulnerable = false;
        HasShield = false;
        activeSword = true;

        crossBow.SetActive(false);
        sword.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) gameLogicManager.GetComponent<GameLogicManager>().LooseGame();
        if (anim.GetCurrentAnimatorClipInfo(0).Length > 0 && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "mixamo.com" && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.505f))
        {
            CreateArrow();
            //audio de fletxa
        }
        Defense = (anim.GetCurrentAnimatorClipInfo(0).Length > 0 && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Human_ShieldWall_Idle" && HasShield);
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        anim.SetFloat("SpeedX", HasShield ? x : x * 2);
        anim.SetFloat("SpeedY", HasShield ? y : y * 2);

        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        if (!HasShield) transform.Translate(new Vector3(x, 0.0f, y) * Time.deltaTime * speed);
        else transform.Translate(new Vector3(x, 0.0f, y) * Time.deltaTime * (speed * 0.5f));
        //Rotation
        float rotationY = Input.GetAxis("Mouse X");
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Physics.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
            IsGrounded = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameLogicManager.GetComponent<LogicManager>().GoToMenu();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            anim.Play("Attack");
        }
        if (Input.GetKeyDown(KeyCode.T) && HasCrossBow && !activeSword)
        {
            anim.Play("Bow");
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (activeSword) anim.Play("Attack"); // audio de espasa
            else if (HasCrossBow && !activeSword)
            {
                Debug.Log("Crossbow shot");
                anim.Play("Bow");
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            anim.Play("Shield");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            invulnerable = !invulnerable;
            anim.Play("Shield");
        }
        if (Input.GetKeyDown(KeyCode.R) && (HasCrossBow))
        {
            activeSword = !activeSword;
            crossBow.SetActive(!activeSword);
            sword.SetActive(activeSword);
        }


    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Floor"){
            IsGrounded = true;
        }
        if(collision.gameObject.tag == "Money Bag"){
            money = money + 10;
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "Health Potion"){
            health = health + 10;
            Destroy(collision.gameObject);
        }
    }

    public void hasShield(bool hasShield){
        HasShield = hasShield;
        anim.SetBool("HasShield", HasShield);
    }   

    public void hasCrossBow(bool hasCrossBow){
        HasCrossBow = hasCrossBow;
    }   

    public bool isAttacking(){
        return (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Human_DualWield_Attack" && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.2f ));
    }

    public void Hit(int damage){
        if((HasShield && Defense) || invulnerable)
            Debug.Log("Hit! with cover");
        else{
            health=health - damage;
        }
            anim.Play("Hit");
            //audio de hit
    }
    
    public int getHealth(){
        return health;
    }
    public void CreateArrow(){
        Instantiate(prefabArrow, crossBow.transform.position, transform.rotation);
    }
    public void CreateSpider(){
        Instantiate(prefabSpider,transform.position, transform.rotation);
    }
}
