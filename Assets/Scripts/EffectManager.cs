using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionSystem;
    [SerializeField] ParticleSystem sparkSystem;
    [SerializeField] ParticleSystem electricitySystem;
    public static EffectManager main;

    private void Awake()
    {
        main = this;
    }
    public void Hit(Vector3 position)
    {
        ParticleSystem.EmitParams par = new ParticleSystem.EmitParams();
        par.position = position;
        explosionSystem.Emit(par, 1);
    }
    public void Explode(Vector3 position)
    {
        ParticleSystem.EmitParams par = new ParticleSystem.EmitParams();
        par.position = position;
        sparkSystem.Emit(par, 10);
        electricitySystem.Emit(par, 10);
        explosionSystem.Emit(par, 10);
    }

    public void Decal(Vector3 pos, Vector3 normal)
    {

    }
}
