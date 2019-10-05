using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]
[DefaultExecutionOrder(-200)]
public class EnemyController : MonoBehaviour
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
    private void LateUpdate()
    {
        bool fighting = false;
        if (target)
        {
            float distance = Vector3.Distance(target.position, this.transform.position);
            if (distance < aimRange)
            {
                controller.AimAt(target.position);
                if (distance < shootRange)
                {
                    if (Random.value < aggressivity) 
                        controller.Fire1();
                    if (Random.value < aggressivity)
                        controller.Fire2();
                    controller.Move(new Vector2(0, 0));
                }
                else if(controller.speed >0)
                {
                    controller.Move(new Vector2((target.position.x - this.transform.position.x)* chaseSpeed, 0));
                }
                fighting = true;

            }

        }
        if (!fighting && patrolRange > 0 && patrolSpeed > 0)
        {
            t += Time.deltaTime;
            controller.Move(new Vector2(Mathf.Sin(t * patrolSpeed) * patrolRange, 0));
            controller.AimAt(transform.right);
        }
    }
}
