﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float tickTime=1;
    //Vector3 position;
    //float randomStartWiggling;
    // Use this for initialization
    void Start()
    {
       // position = transform.localPosition;
      //  randomStartWiggling = Random.value * 10;
    }
    void LateUpdate()
    {
      //  this.transform.localPosition = position + new Vector3(0, Mathf.Sin(Time.time * 6 + randomStartWiggling) * 0.05f, 0);
    }

    float t;
    void OnTriggerEnter(Collider other)
    {
        t = 0;
       //Debug.Log("collision");
        if (other.gameObject.tag == "Player")
        {
            Controller controller = other.gameObject.GetComponent<Controller>();
            if (controller != null)
            {
                controller.Damage(damage);
                //controller.Push(-(this.transform.position - controller.transform.position));
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
       

        if (other.gameObject.tag == "Player")
        {
            Controller controller = other.gameObject.GetComponent<Controller>();
            if (controller != null)
            {
                t += Time.deltaTime;
                if (t > tickTime)
                {
                    t -= tickTime;
                    controller.Damage(damage);
                    //controller.Push(-(this.transform.position - controller.transform.position));
                }
            }
        }
        
    }
}
