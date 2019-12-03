﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Equipment;

public class BoltAnim : Particle
{
    [Header("Particle Attributes")]
    public Gradient color;
    private Renderer render;

    protected override void DoCreate()
    {
       if(!render) render = GetComponent<MeshRenderer>();
    }
    protected override void DoTick()
    {
        if(render != null)
            render.material.color = color.Evaluate(1 - time / lifeTime);
    }

    protected override void DoDestroy()
    {
        gameObject.SetActive(false);
    }
}
