using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    public GameObject player;

    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;

    private GameObject key1;
    private GameObject key2;
    private GameObject key3;
    private GameObject moneyCounter;
    private GameObject ballistaImage;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        key1 = GameObject.Find("Key1Image");
        key2 = GameObject.Find("Key2Image");
        key3 = GameObject.Find("Key3Image");
        moneyCounter = GameObject.Find("CoinNumber");
        ballistaImage = GameObject.Find("BallistaImage");
    }

    // Update is called once per frame
    void Update()
    {
        updateKeys();

        updateHealth();

        updateMoney();

        updateWeapon();
    }

    private void updateWeapon()
    {

        if (player.GetComponent<MainPlayerLogics>().HasCrossBow) ballistaImage.GetComponent<Image>().color = new Color(255, 255, 255, 100);

    }

    private void updateKeys()
    {
        if (player.GetComponent<PlayerLogic>().keys[0]) key1.GetComponent<Image>().color = new Color(255, 255, 255, 100);
        if (player.GetComponent<PlayerLogic>().keys[1]) key2.GetComponent<Image>().color = new Color(255, 255, 255, 100);
        if (player.GetComponent<PlayerLogic>().keys[2]) key3.GetComponent<Image>().color = new Color(255, 255, 255, 100);
    }

    private void updateHealth()
    {
        int x = player.GetComponent<MainPlayerLogics>().health;

        if(x > 100) x = 100;
        if (x < 0) x = 0;

        int[] hearts = {0,0,0,0,0};

        int n_hearts = x / 20;
        int n_halves = x % 20;

        for (int i = 0; i < n_hearts; i++)
        {
            hearts[i] = 2;
        }

        if (n_hearts != 5 && n_halves !=0) //out of bounds check
        {
            if (n_halves <= 10)
            {
                hearts[n_hearts] = 1;
            }
            if (n_halves > 10)
            {
                hearts[n_hearts] = 2;
            }
        }
 
        paintHearts(hearts);

    }

    private void paintHearts(int[] hearts)
    {
        for(int i = 0; i < hearts.Length; ++i)
        {
            switch (hearts[i]){
                case 0:
                    GameObject.Find("Heart" + i).GetComponent<Image>().sprite = heartEmpty;
                    break;
                case 1:
                    GameObject.Find("Heart" + i).GetComponent<Image>().sprite = heartHalf;
                    break;
                case 2:
                    GameObject.Find("Heart" + i).GetComponent<Image>().sprite = heartFull;
                    break;
            }
        }
    }

    private void updateMoney()
    {
        int num = player.GetComponent<MainPlayerLogics>().money;
        moneyCounter.GetComponent<Text>().text = num.ToString();
    }

    

    
}
