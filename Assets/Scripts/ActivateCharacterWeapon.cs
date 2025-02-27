using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCharacterWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public int weaponNumber;
    public string weaponType;
    public PickUpWeapons pickUpWeapons;

    public int rotationSpeed = 20;

    void Start()
    {
        pickUpWeapons = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpWeapons>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime), Space.World);
    }
    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            pickUpWeapons.ActivateWeapons(weaponNumber, weaponType);
            Destroy(gameObject);
        }
    }
}
