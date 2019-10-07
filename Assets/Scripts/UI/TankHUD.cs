using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHUD : MonoBehaviour
{
    [Header("Followed items")]
    [SerializeField] public Controller playerController;
    [SerializeField] public Game game;

    [Header("Internal elements")]
    [SerializeField] public SpriteMask lifeBar;
    [SerializeField] public SpriteMask energyBar;
    [SerializeField] public SpriteMask ammoBar;
    [SerializeField] public SpriteRenderer crossedEnergyBar;
    [SerializeField] public SpriteRenderer crossedAmmoBar;
    [SerializeField] public GameObject death;
    [SerializeField] public GameObject conversion;
    [SerializeField] public GameObject won;
    [SerializeField] GameObject passive;
    [SerializeField] Radar radar;

    [SerializeField] Counter bonus;
    [SerializeField] Counter alignment;

    [SerializeField] GameObject alignmentChangeUp;
    [SerializeField] GameObject alignmentChangeDown;

    // Update is called once per frame
    void Update()
    {
        if(game.death)
        {
            // hide all except death screen
            energyBar.transform.parent.gameObject.SetActive(false);
            ammoBar.transform.parent.gameObject.SetActive(false);
            lifeBar.transform.parent.gameObject.SetActive(false);
            death.SetActive(true);
            won.SetActive(false);
        }
        else if (game.conversion)
        {
            // hide all except death screen
            energyBar.transform.parent.gameObject.SetActive(false);
            ammoBar.transform.parent.gameObject.SetActive(false);
            lifeBar.transform.parent.gameObject.SetActive(false);
            conversion.SetActive(true);
            won.SetActive(false);
        }
        else if(game.won)
        {
            won.SetActive(true);
        }
        else
        {
            // life bar
            lifeBar.transform.parent.gameObject.SetActive(true);
            lifeBar.alphaCutoff = 1.0f - Mathf.Clamp((float)playerController.hp / playerController.maxHp, 0.0f, 1.0f);

            // energy bar
            energyBar.transform.parent.gameObject.SetActive(playerController.equipement.secondaryWeapon != null);
            crossedEnergyBar.gameObject.SetActive((playerController.equipement.secondaryWeapon != null && playerController.equipement.secondaryWeapon.overheat));
            if (playerController.equipement.secondaryWeapon != null)
            {
                energyBar.alphaCutoff = 1.0f - (float)playerController.equipement.secondaryWeapon.load / playerController.equipement.secondaryWeapon.capacity;
            }
            else
            {
                energyBar.alphaCutoff = 1.0f;
            }

            //  standard ammo bar
            ammoBar.transform.parent.gameObject.SetActive(playerController.equipement.weapon != null);
            crossedAmmoBar.gameObject.SetActive((playerController.equipement.weapon != null && playerController.equipement.weapon.overheat));
            if (playerController.equipement.weapon != null)
            {
                ammoBar.alphaCutoff = 1.0f - (float)playerController.equipement.weapon.load / playerController.equipement.weapon.capacity;
            }
            else
            {
                ammoBar.alphaCutoff = 0.0f;
            }
            passive.gameObject.SetActive(playerController.equipement.passive != null);
            radar.gameObject.SetActive((playerController.equipement.passive != null && playerController.equipement.passive.type == Passive.Type.Radar));

            bonus.gameObject.SetActive(playerController.points > 0);
            if (playerController.points > 0)
            {
                bonus.Count = playerController.points;
            }

            
            int alignmentChange = 0;
            if (playerController.hoveredObject != null)
            {
                alignmentChange = playerController.equipement.GetAlignmentChange(playerController.hoveredObject.toPick);
                alignmentChangeUp.SetActive(alignmentChange > 0);
                alignmentChangeDown.SetActive(alignmentChange < 0);
            
            }
            else
            {
                alignmentChangeUp.SetActive(false);
                alignmentChangeDown.SetActive(false);
            }
            int alignmentValue = playerController.equipement.GetAlignment();
            alignment.gameObject.SetActive(alignmentValue > 0);
            if (alignmentChange!= 0 || alignmentValue > 0)
            {
                alignment.Count = alignmentValue;
            }
        }
    }
}
