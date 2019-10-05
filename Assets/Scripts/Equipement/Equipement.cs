using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipement : MonoBehaviour
{
    // Attributes
    [Header("Current equipement")]
    public Weapon weapon;
    public SecondaryWeapon secondaryWeapon;
    public Shield shield;
    public Scan scan;
    public Passive passive;

    [Header("Possible equipement")]
    public Weapon[] weaponList;
    public SecondaryWeapon[] secondaryWeaponList;
    public Shield[] shieldList;
    public Scan[] scanList;
    public Passive[] passiveList;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Public functions
    public void SetWeapon(Weapon.Type type)
    {

    }
}
