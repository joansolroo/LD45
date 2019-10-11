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

    float t;

    Vector3 lastKnownPosition;
    Vector3 currentPosition;
    Vector3 direction;
    Vector3 currentDirection;
    float distance;

    /*void Start ()
    {
        target = PlayerController.main.transform;
    }
    private void Update()
    {
        Sense();
        Think();
        Act();
    }*/

    public void Sense()
    {
        if (target == null)
        {
            if (Random.value < 0.05f)
            {
                float a = Random.Range(0, Mathf.Deg2Rad * 360);

                direction = new Vector3(Mathf.Sin(a), 0, Mathf.Cos(a));
                lastKnownPosition = this.transform.position + direction;
            }
        }
        else
        {
            currentPosition = target.position;
            Vector3 newDirection = currentPosition - this.transform.position;
            distance = newDirection.magnitude;
            if (distance < aimRange)
            {
                lastKnownPosition = currentPosition;
                direction = newDirection.normalized;
            }
            else
            {
                if (Random.value < 0.05f)
                {
                    float a = Random.Range(0, Mathf.Deg2Rad * 360);

                    direction = new Vector3(Mathf.Sin(a), 0, Mathf.Cos(a));
                    lastKnownPosition = this.transform.position + direction;
                }
            }
        }
    }
    
    public void Act()
    {
        currentDirection = Vector3.MoveTowards(currentDirection, direction, Time.deltaTime);
        controller.Move(Vector3.zero);
        bool fighting = false;
        if (target) // disable friendly aiming and fire
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

                    if(distance> shootRange/2)
                    {
                        controller.Move(currentDirection);
                    }
                }
                else if (controller.speed > 0)
                {
                    controller.Move(currentDirection);
                }
                fighting = true;
            }
            if (target.gameObject.layer == 10)
                target = null;
        }
        if (!fighting && patrolRange > 0 && patrolSpeed > 0)
        {
            t += Time.deltaTime;
            controller.Move(currentDirection);
        }
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
