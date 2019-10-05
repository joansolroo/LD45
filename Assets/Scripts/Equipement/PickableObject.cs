﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour {

    [SerializeField] public Component toPick;

    Vector3 position;
    float randomStartWiggling;
    // Use this for initialization
    void Start()
    {
        position = transform.localPosition;
        randomStartWiggling = Random.value * 10;
    }
    void LateUpdate()
    {
        this.transform.localPosition = position + new Vector3(0, Mathf.Sin(Time.time * 6+ randomStartWiggling) *0.01f, 0);
        triggered = false;
    }

    bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        HandleTrigger(other, false);
    }
    private void OnTriggerStay(Collider other)
    {
        HandleTrigger(other, false);
    }

    private void OnTriggerExit(Collider other)
    {
        HandleTrigger(other, true);
    }

    private void HandleTrigger(Collider other, bool end)
    {
        if (!triggered)
        {
            triggered = true;
            if (other.gameObject.tag == "Player")
            {
                Controller controller = other.gameObject.GetComponent<Controller>();
                if (controller != null)
                {
                    bool success = controller.OnPickObject(this,end);

                    if (success)
                    {
                        Destroy();
                    }
                }
            }
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
