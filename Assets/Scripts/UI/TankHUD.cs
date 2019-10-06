using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHUD : MonoBehaviour
{
    [Header("Followed items")]
    [SerializeField] public Controller playerController;

    [Header("Internal elements")]
    [SerializeField] public SpriteMask lifeBar;
    [SerializeField] public SpriteMask energyBar;
    [SerializeField] public SpriteMask ammoBar;
    [SerializeField] public SpriteRenderer crossedEnergyBar;
    [SerializeField] public SpriteRenderer crossedAmmoBar;

    // Update is called once per frame
    void Update()
    {
        // update player components
        lifeBar.alphaCutoff = 1.0f - Mathf.Clamp((float)playerController.hp / playerController.maxHp, 0.0f, 1.0f);

        // energy bar
        crossedEnergyBar.enabled = (playerController.equipement.secondaryWeapon == null);
        if (playerController.equipement.secondaryWeapon != null)
        {
            energyBar.alphaCutoff = 1.0f - (float)playerController.equipement.secondaryWeapon.load / playerController.equipement.secondaryWeapon.capacity;
        }
        else
        {
            energyBar.alphaCutoff = 1.0f;
        }

        //  standard ammo bar
        crossedAmmoBar.enabled = (playerController.equipement.weapon == null);
        if (playerController.equipement.weapon != null)
        {
            ammoBar.alphaCutoff = 1.0f - (float)playerController.equipement.weapon.load / playerController.equipement.weapon.capacity;
        }
        else
        {
            ammoBar.alphaCutoff = 0.0f;
        }
    }
}
