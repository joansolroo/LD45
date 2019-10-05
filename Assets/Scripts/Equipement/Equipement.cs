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

    // Get
    public int GetAlignment()
    {
        int alignment = 0;
        if(weapon != null)
        {
            alignment += weapon.alignment;
        }
        if (secondaryWeapon != null)
        {
            alignment += secondaryWeapon.alignment;
        }
        if (shield != null)
        {
            alignment += shield.alignment;
        }
        if (scan != null)
        {
            alignment += scan.alignment;
        }
        if (passive != null)
        {
            alignment += passive.alignment;
        }
        return alignment;
    }
    public int GetAlignmentChange(Component change)
    {
        int alignment = 0;
        if (change != null)
        {
            if (change.GetType() == typeof(Weapon))
                alignment = ((Weapon)change).alignment - (weapon != null ? weapon.alignment : 0);
            else if (change.GetType() == typeof(SecondaryWeapon))
                alignment = ((SecondaryWeapon)change).alignment - (secondaryWeapon != null ? secondaryWeapon.alignment : 0);
            else if (change.GetType() == typeof(Shield))
                alignment = ((Shield)change).alignment - (shield != null ? shield.alignment : 0);
            else if (change.GetType() == typeof(Scan))
                alignment = ((Scan)change).alignment - (scan != null ? scan.alignment : 0);
            else if (change.GetType() == typeof(Passive))
                alignment = ((Passive)change).alignment - (passive != null ? passive.alignment : 0);
        }
        return alignment;
    }
}
