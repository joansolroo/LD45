using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]
[DefaultExecutionOrder(-200)]
public class EnemyController : MonoBehaviour, IIndividual
{
    [SerializeField] Controller controller;

    public Transform target;
    [SerializeField] float aggressivity = 0.5f;
    [SerializeField] float aimRange = 5f;
    [SerializeField] float shootRange = 4f;
    [SerializeField] float patrolRange = 0f;
    [SerializeField] float patrolSpeed = 1f;
    [SerializeField] float chaseSpeed = 1f;

    float t;

    Vector3 lastKnownPosition;
    Vector3 currentPosition;
    Vector3 direction;
    float distance;

    private void Update()
    {
        Sense();
        Think();
        Act();
    }

    public void Sense()
    {

        currentPosition = target.position;
        direction = currentPosition - this.transform.position;
        distance = direction.magnitude;
        if (distance < aimRange)
        {
            lastKnownPosition = currentPosition;
        }
    }

    public void Think()
    {
       
    }

    public void Act()
    {

        controller.Move(Vector3.zero);
        bool fighting = false;
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
                else if (controller.speed > 0)
                {
                    controller.Move(direction.normalized);
                }
                fighting = true;

            }

        }
        if (!fighting && patrolRange > 0 && patrolSpeed > 0)
        {
            t += Time.deltaTime;
            //controller.Move(new Vector2(Mathf.Sin(t * patrolSpeed) * patrolRange, 0));
            controller.AimAt(transform.right);
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
