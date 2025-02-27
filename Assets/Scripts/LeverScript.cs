using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public bool moving;
    public bool pulled;
    public GameObject chest1;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.x > 0) pulled = true;
        if (moving && !pulled)
        {
            transform.RotateAround(transform.position, new Vector3(0.0f, 0.0f, -1.0f), 20 * Time.deltaTime);
        }
        if (pulled) chest1.GetComponent<ChestLogic>().unlock();

    }

    public void PullLever()
    {
        //start moving
        if(!pulled) moving = true;
        
    }
}
