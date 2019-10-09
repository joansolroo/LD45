using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltAnim : MonoBehaviour
{
    [Header("Particle Attributes")]
    public float lifeTime;
    public Gradient color;
    private Renderer render;

    [Header("Debug")]
    public float life;
    private float startLife;

    private void Start()
    {
        startLife = life;
    }
    public void Init()
    {
        life = lifeTime;
        render = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        life -= Time.deltaTime;
        if(render != null)
            render.material.color = color.Evaluate(1 - life / lifeTime);
        if(life <= 0)
            gameObject.SetActive(false);
    }
    private void Reset()
    {
        life = startLife;
    }
}
