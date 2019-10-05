using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]
[DefaultExecutionOrder(-200)]
public class EnemySupport : MonoBehaviour, IIndividual
{
    [SerializeField] Controller controller;
    [SerializeField] Sense senseFriends;
    [SerializeField] Sense sensePlayer;

    [SerializeField] float seeDistance = 3f;
    [SerializeField] float fleeDistance = 2f;
    [SerializeField] float friendDistance = 5f;


    [Header("debug")]
    public Transform player;
    public Transform friend;
    Vector3 lastKnownPlayerPosition;
    Vector3 playerDirection;
    float distance;
    private void Update()
    {
        Sense();
        Think();
        Act();
    }

    public void Sense()
    {
        if (player == null || !sensePlayer.IsVisible(player.gameObject))
        {
            if (sensePlayer.perceived.Count > 0)
            {
                player = sensePlayer.GetRandom().transform;
            }
            else
            {
                player = null;
            }
        }

        if (player)
        {
            
            Vector3 currentPlayerPosition = player.position;
            playerDirection = currentPlayerPosition - this.transform.position;
            distance = playerDirection.magnitude;
            if (distance < seeDistance)
            {
                lastKnownPlayerPosition = currentPlayerPosition;
            }

        }

        if (friend == null || !senseFriends.IsVisible(friend.gameObject))
        {
            if (senseFriends.perceived.Count > 0)
            {
                try
                {
                    friend = senseFriends.GetClosest(this.transform.position).transform;
                }
                catch(System.Exception)
                { friend = null;
                }
            }
            else
            {
                friend = null;
            }
        }
    }

    public void Think()
    {

    }

    public void Act()
    {
        
        controller.Move(Vector3.zero);
        bool fighting = false;
        if (friend)
        {
            controller.Move(friend.transform.position-this.transform.position);
            controller.AimAt(friend.transform.position);
        }
        else if (player)
        {
            if (distance < fleeDistance)
            {

                if (controller.speed > 0)
                {
                    playerDirection.y = 0;
                    controller.Move(-playerDirection.normalized);
                }
                fighting = true;
            }

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
