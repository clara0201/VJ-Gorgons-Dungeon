using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int health, money;
    public bool[] keys = { false, false, false };
    public bool weapon1, weapon2, invulnerable, hasCrossbow, swordActive;

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);


        health = 100;
        swordActive = true;
    }

}
