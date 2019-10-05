using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("Followed player")]
    [SerializeField] public Controller playerController;

    [Header("Internal elements")]
    [SerializeField] public UIBar lifeBar;
    [SerializeField] public UIBar energyBar;
    [SerializeField] public UIBar ammoBar;
    [SerializeField] public Image shieldIcon;

    // Update is called once per frame
    void Update()
    {
        // update player components
        lifeBar.value = Mathf.Clamp((float)playerController.hp / playerController.maxHp, 0.0f, 1.0f);
        energyBar.value = Mathf.Clamp((float)playerController.energy / playerController.maxEnergy, 0.0f, 1.0f);
        if(playerController.equipement.weapon != null)
        {
            ammoBar.label.gameObject.SetActive(false);
            ammoBar.shadow.gameObject.SetActive(true);
            ammoBar.foreground.gameObject.SetActive(true);
            ammoBar.value = (float)playerController.equipement.weapon.load / playerController.equipement.weapon.capacity;
        }
        else
        {
            ammoBar.label.gameObject.SetActive(true);
            ammoBar.shadow.gameObject.SetActive(false);
            ammoBar.foreground.gameObject.SetActive(false);
        }
        shieldIcon.gameObject.SetActive(playerController.equipement.shield != null);
    }
}
