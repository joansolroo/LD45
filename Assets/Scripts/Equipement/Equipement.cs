﻿using System.Collections;
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

    // Set specific equipement
    public void SetWeapon(Weapon.Type type)
    {
        weapon = null;
        foreach (Weapon w in weaponList)
        {
            if (w.type == type)
            {
                w.gameObject.SetActive(true);
                weapon = w;
            }
            else
            {
                w.gameObject.SetActive(false);
            }
        }
    }
    public void SetSecondaryWeapon(SecondaryWeapon.Type type)
    {
        secondaryWeapon = null;
        foreach (SecondaryWeapon sw in secondaryWeaponList)
        {
            if (sw.type == type)
            {
                sw.gameObject.SetActive(true);
                secondaryWeapon = sw;
            }
            else
            {
                sw.gameObject.SetActive(false);
            }
        }
    }
    public void SetShield(Shield.Type type)
    {
        shield = null;
        foreach (Shield s in shieldList)
        {
            if (s.type == type)
            {
                s.gameObject.SetActive(true);
                shield = s;
            }
            else
            {
                s.gameObject.SetActive(false);
            }
        }
    }
    public void SetScan(Scan.Type type)
    {
        scan = null;
        foreach (Scan s in scanList)
        {
            if (s.type == type)
            {
                s.gameObject.SetActive(true);
                scan = s;
            }
            else
            {
                s.gameObject.SetActive(false);
            }
        }
    }
    public void SetPassive(Passive.Type type)
    {
        passive = null;
        foreach (Passive p in passiveList)
        {
            if (p.type == type)
            {
                p.gameObject.SetActive(true);
                passive = p;
            }
            else
            {
                p.gameObject.SetActive(false);
            }
        }
    }


}
