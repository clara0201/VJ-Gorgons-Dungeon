using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLogic : MonoBehaviour
{
    public bool pushed = false;
    public GameObject chest;
    public GameObject switchButton;
    public GameObject bridge;

    private bool moveBridge;

    private void Update()
    {
        if (moveBridge && bridge.transform.position.z < -26.0f) 
        {
            bridge.transform.Translate(new Vector3(0.0f, 0.0f, 0.1f));
        }
    }

    public void push()
    {
        if(!pushed)
        {
            pushed = true;
            switchButton.transform.Translate(new Vector3(0.0f,0.0f,-1.0f));
            chest.GetComponent<ChestLogic>().unlock();
            moveBridge = true;
        }
    }


}
