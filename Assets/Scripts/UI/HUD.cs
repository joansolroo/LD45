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

    [SerializeField] public GameObject alignmentPanel;
    [SerializeField] public UIBar currentAlignment;
    [SerializeField] public UIBar futurAlignment;
    [SerializeField] public Image direction;

    private int maxAlignment = 10;

    
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

        // algnment
        if (playerController.hoveredObject != null)
        {
            int a = playerController.equipement.GetAlignment();
            int da = playerController.equipement.GetAlignmentChange(playerController.hoveredObject);
            alignmentPanel.SetActive(true);
            currentAlignment.value = (float)a / maxAlignment;
            futurAlignment.value = (float)(a+da) / maxAlignment;
            if (da > 0) direction.rectTransform.localEulerAngles = new Vector3(0, 0, 0);
            else direction.rectTransform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else alignmentPanel.SetActive(false);
    }
}
