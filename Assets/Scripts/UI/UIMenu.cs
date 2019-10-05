using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [SerializeField] Canvas GameUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideMenu()
    {
        this.gameObject.SetActive(false);
        GameUI.gameObject.SetActive(true);
    }
}
