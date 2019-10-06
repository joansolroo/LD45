using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(200)]
public class GuidingSystem : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] SpriteRenderer targetDisplay;
    [SerializeField] Transform target;
    public Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        bullet.targetPosition = targetPos;
        targetDisplay.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (target)
        {
            targetPos = target.position;
            bullet.targetPosition = targetPos;
        }
    }
    public void Guide(Bullet b)
    {
        bullet = b;
        if (target)
        {
            targetPos = target.position;
        }
        bullet.guided = true;
        bullet.targetPosition = targetPos;
        targetDisplay.gameObject.SetActive(true);

    }
    // Update is called once per frame
    void LateUpdate()
     {
        if (bullet)
         {
            
            targetPos.y = 0;
             targetDisplay.transform.position = targetPos;
             targetDisplay.transform.eulerAngles = new Vector3(90, 0, 0);
         }
        else
        {
            targetDisplay.gameObject.SetActive(false);
        }

     }
}
