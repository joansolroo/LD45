using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 orientation;
    public static Cursor main;
    // Start is called before the first frame update
    private void Awake()
    {
        main = this;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = targetPosition;
        this.transform.eulerAngles = orientation;
        /*
        float cursorSpeedFactor = 10;
        float stretchiness = 0.1f;
        Vector3 cursorDirection = this.transform.position - targetPosition;
        if (cursorDirection.sqrMagnitude > 0.1f)
        {
            float cursorSpeed = Mathf.Max(1, Vector3.Distance(this.transform.position, targetPosition));
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, cursorSpeedFactor * cursorSpeed * Time.deltaTime);
            Vector3 scale = Vector3.one + new Vector3(-cursorDirection.sqrMagnitude, 0, +cursorDirection.sqrMagnitude) * stretchiness;
            scale.x = Mathf.Clamp(scale.x, 0.25f, 10f);
            scale.y = 1;
            scale.z = Mathf.Clamp(scale.z, 0.25f, 10f);
            this.transform.localScale = scale;
            this.transform.LookAt(targetPosition);
        }
        else
        {
            this.transform.position = targetPosition;
            this.transform.localEulerAngles = Vector3.zero;
            this.transform.localScale = Vector3.one;
        }*/
    }
}
