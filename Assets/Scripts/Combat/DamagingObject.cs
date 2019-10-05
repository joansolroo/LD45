using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] bool isGhost = false;

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
        this.transform.localPosition = position + new Vector3(0, Mathf.Sin(Time.time * 6 + randomStartWiggling) * 0.05f, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != this.gameObject.tag)
        {
            Controller controller = collision.gameObject.GetComponent<Controller>();
            if (controller != null)
            {
                controller.Damage(damage);
                controller.Push(-(this.transform.position - controller.transform.position));
            }
        }

    }
}
