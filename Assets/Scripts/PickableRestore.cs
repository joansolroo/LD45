using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableRestore : MonoBehaviour
{
    public enum Type
    {
        None, HP, Energy, Ammo
    }
    public Type pickableType;
    public GameObject renderer;

    private void Update()
    {
        renderer.transform.Rotate(0, 60 * Time.deltaTime, 0,Space.World);
    }
}
