using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarArrow : MonoBehaviour
{
    [SerializeField] SpriteRenderer contour;
    [SerializeField] SpriteRenderer background;
    [SerializeField] float visibility = 0;


    public void SetVisibility(float v)
    {
        visibility = v;
        Color cc = contour.color;
        cc.a = v;
        contour.color = cc;

        Color cb = background.color;
        cb.a = v;
        background.color = cb;
    }
}
