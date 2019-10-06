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

    [Header("Equipement change")]
    public GameObject pickablePrefab;
    public GameObject pickablePrefabSW;
    [Range(0.0f, 10f)] public float pushAmplitude;

    // Set specific equipement
    public void SetWeapon(Weapon.Type type)
    {
        DropWeapon(weapon);
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
        DropSecondaryWeapon(secondaryWeapon);
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
        if (change != null)
        {
            if (change.GetType() == typeof(Weapon))
                return ((Weapon)change).alignment - (weapon != null ? weapon.alignment : 0);
            else if (change.GetType() == typeof(SecondaryWeapon))
                return ((SecondaryWeapon)change).alignment - (secondaryWeapon != null ? secondaryWeapon.alignment : 0);
            else if (change.GetType() == typeof(Shield))
                return ((Shield)change).alignment - (shield != null ? shield.alignment : 0);
            else if (change.GetType() == typeof(Scan))
                return ((Scan)change).alignment - (scan != null ? scan.alignment : 0);
            else if (change.GetType() == typeof(Passive))
                return ((Passive)change).alignment - (passive != null ? passive.alignment : 0);
        }
        return 0;
    }

    // private
    private void DropWeapon(Weapon w)
    {
        if (w == null) return;

        GameObject drop = GameObject.Instantiate(pickablePrefab);
        drop.transform.position = transform.position + 3 * Vector3.up;
        drop.SetActive(true);
        Vector2 u = pushAmplitude * new Vector2(Random.Range(-pushAmplitude, pushAmplitude), Random.Range(-pushAmplitude, pushAmplitude)).normalized;
        drop.GetComponent<Rigidbody>().velocity = Vector3.up + u.x * Vector3.left + u.y * Vector3.right;

        GameObject PickableEmpty = drop.transform.Find("PickableEmpty").gameObject;
        Weapon targetWeapon = PickableEmpty.GetComponent<Weapon>();
        targetWeapon.type = w.type;
        targetWeapon.alignment = w.alignment;

        if (w.iconPrefab != null)
        {
            GameObject icon = Instantiate(w.iconPrefab);
            icon.transform.parent = PickableEmpty.transform.Find("Slot");
            icon.transform.localPosition = new Vector3(0, 0, 0);
            icon.transform.localRotation = Quaternion.identity;
            icon.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void DropSecondaryWeapon(SecondaryWeapon w)
    {
        if (w == null)
        {
            Debug.LogWarning("w is null");
            return;

        }
        GameObject drop = GameObject.Instantiate(pickablePrefabSW);
        drop.transform.position = transform.position + 3 * Vector3.up;
        drop.SetActive(true);
        Vector2 u = pushAmplitude * new Vector2(Random.Range(-pushAmplitude, pushAmplitude), Random.Range(-pushAmplitude, pushAmplitude)).normalized;
        drop.GetComponent<Rigidbody>().velocity = Vector3.up + u.x * Vector3.left + u.y * Vector3.right;

        GameObject PickableEmptySW = drop.transform.Find("PickableEmptySW").gameObject;
        SecondaryWeapon targetWeapon = PickableEmptySW.GetComponent<SecondaryWeapon>();
        targetWeapon.type = w.type;
        targetWeapon.alignment = w.alignment;

        if (w.iconPrefab != null)
        {
            GameObject icon = Instantiate(w.iconPrefab);
            icon.transform.parent = PickableEmptySW.transform.Find("Slot");
            icon.transform.localPosition = new Vector3(0, 0, 0);
            icon.transform.localRotation = Quaternion.identity;
            icon.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
