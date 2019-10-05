using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]
[DefaultExecutionOrder(-200)]
public class EnemyTurret : MonoBehaviour, IIndividual
{
    [SerializeField] Controller controller;
    [SerializeField] Sense sight;
    public Transform target;
    [SerializeField] float aggressivity = 0.5f;
    [SerializeField] float aimRange = 5f;
    [SerializeField] float shootRange = 4f;


    float t;

    [Header("debug")]
    [SerializeField] Vector3 lastKnownPosition;
    [SerializeField] Vector3 currentPosition;
    [SerializeField] Vector3 direction;
    [SerializeField] float distance;

    private void Start()
    {
        sight.transform.localScale = Vector3.one * aimRange/2;
    }
    private void Update()
    {
        Sense();
        Think();
        Act();
    }

    public void Sense()
    {
        if (target == null || !sight.IsVisible(target.gameObject))
        {
            if (sight.perceived.Count > 0)
            {
                target = sight.GetRandom().transform;
            }
            else
            {
                target = null;
            }
        }

        if (target)
        {
            currentPosition = target.position;
            direction = currentPosition - this.transform.position;
            distance = direction.magnitude;
            if (distance < aimRange)
            {
                lastKnownPosition = currentPosition;
            }
        }
    }

    public void Think()
    {

    }

    public void Act()
    {

        //controller.Move(Vector3.zero);
        if (target)
        {
            if (distance < aimRange)
            {
                controller.AimAt(lastKnownPosition);
                if (distance < shootRange)
                {
                    if (Random.value < aggressivity)
                        controller.Fire1();
                    if (Random.value < aggressivity)
                        controller.Fire2();
                }
            }

        }
        else
        {
            t += Time.deltaTime;
            //controller.Move(new Vector2(Mathf.Sin(t * patrolSpeed) * patrolRange, 0));
            float a = Time.time;
            controller.AimAt(this.transform.position+new Vector3(Mathf.Sin(a),0,Mathf.Cos(a)));
        }
    }

    public void Display()
    {

    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public bool Alive()
    {
        return controller.alive;
    }
}
