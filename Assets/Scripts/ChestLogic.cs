using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLogic : MonoBehaviour
{
    public bool locked = true;
    public bool opened = false;
    public float aux = -90.0f;
    
    public GameObject box;
    public GameObject lid;
    public GameObject key;
    public GameObject chestLock;
    public ParticleSystem part;
    public GameObject chestHint;

    // Start is called before the first frame update
    void Start()
    {
        key.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void unlock()
    {
        locked = false;
        Destroy(chestLock);
        chestHint.SetActive(false);
        
    }

    public void open()
    {
        if (!locked)
        {
            opened = true;
            part.Play();
            Destroy(box);
            Destroy(lid);
            key.SetActive(true);
        }
        else
        {
            //display 'chest is locked' message
            chestHint.SetActive(true);
        }
        
    }
}
