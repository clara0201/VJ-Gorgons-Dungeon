using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    public float xSpeed = 500; 
    private Rigidbody _rigidbody;
    public GorgonBehaviour gorgonBehaviour;

    // public float ySpeed = -1.5f; 
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if(GameObject.FindGameObjectWithTag("Gorgon") != null)
            gorgonBehaviour = GameObject.FindGameObjectWithTag("Gorgon").GetComponent<GorgonBehaviour>();
    }

    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Spider"){
            collision.gameObject.GetComponent<EnemyBehaviour>().Hit();
            Debug.Log("spider! on arrrow"); 
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Harpy"){
            collision.gameObject.GetComponent<HarpyBehaviour>().Hit();
            Debug.Log("Harpy! on arrrow"); 
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Minotaur"){
            collision.gameObject.GetComponent<MinotaurBehaviour>().Hit();
            Debug.Log("minotaur! on arrrow"); 
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Satyr"){
            collision.gameObject.GetComponent<SatyrBehaviour>().Hit();
            Debug.Log("minotaur! on arrrow"); 
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Eye1" || collision.gameObject.tag == "Eye2"){
            gorgonBehaviour.EyeHit();
            Debug.Log("gorgon Eye! on arrrow"); 
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Gorgon"){
            gorgonBehaviour.Hit();
            Debug.Log("gorgon  on arrrow"); 
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Switch")
        {
            _rigidbody.isKinematic = true;
            xSpeed = 0;
            collision.gameObject.GetComponent<SwitchLogic>().push();
        }
        else if(collision.gameObject.tag != "Player")
        {
            _rigidbody.isKinematic = true;
            xSpeed = 0;
        }
        // ySpeed = 0;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0.0f, 0.0f, 1) * xSpeed * Time.deltaTime);
        
    }
}
