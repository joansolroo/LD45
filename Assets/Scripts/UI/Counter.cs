using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] TextMesh text;
    [SerializeField] int count = 0;
    [SerializeField] string format = "00";
    [SerializeField] bool signed = false;
    public int Count

    {
        get
        {
            return count;
        }
        set
        {
            if (count != value)
            {
                count = value;
                UpdateVisuals();

            }
        }
    }

    void UpdateVisuals (){
        if (signed && count > 0)
        {
            text.text = '+'+count.ToString(format);
        }
        else
        {
            text.text = count.ToString(format);
        }
    }
}
