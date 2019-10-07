using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunAnim : MonoBehaviour
{
    [SerializeField] public Weapon weapon;
    [SerializeField] public Transform pivot1;
    [SerializeField] public Transform pivot2;
    [SerializeField] public LineRenderer[] flamesRight;
    [SerializeField] public LineRenderer[] flamesLeft;
    [SerializeField] public LineRenderer[] cannonRight;
    [SerializeField] public LineRenderer[] cannonLeft;
    [SerializeField] public Transform ejectRight;
    [SerializeField] public Transform ejectLeft;
    [SerializeField] public GameObject boltPrefab;
    [SerializeField] [Range(500, 1000)] public float speed;
    [SerializeField] [Range(0, 10)] public float ejectSpeed;

    private int lastIndexRight, lastIndexLeft;

    private void Start()
    {
        lastIndexRight = Random.Range(0, flamesRight.Length);
        lastIndexLeft = Random.Range(0, flamesLeft.Length);
    }

    public void BulletShot()
    {

    }
    void Update()
    {
        //  rotate cannons
        if(weapon.firing && !weapon.overheat && !weapon.reloading)
        {
            pivot1.localEulerAngles += new Vector3(0, 0,  speed * Time.deltaTime);
            pivot2.localEulerAngles += new Vector3(0, 0, -speed * Time.deltaTime);
        }

        //  flames
        foreach(LineRenderer f in flamesRight)
            f.enabled = false;
        foreach (LineRenderer f in flamesLeft)
            f.enabled = false;
        foreach (LineRenderer f in cannonRight)
            f.enabled = false;
        foreach (LineRenderer f in cannonLeft)
            f.enabled = false;
        if (weapon.firing && weapon.flame)
        {
            lastIndexRight += Random.Range(0, flamesLeft.Length - 1);
            lastIndexRight %= flamesLeft.Length;
            flamesLeft[lastIndexRight].enabled = true;
            cannonLeft[lastIndexRight].enabled = true;

            lastIndexLeft += Random.Range(0, flamesRight.Length - 1);
            lastIndexLeft %= flamesRight.Length;
            flamesRight[lastIndexLeft].enabled = true;
            cannonRight[lastIndexLeft].enabled = true;

            GameObject b1 = Instantiate(boltPrefab);
            b1.GetComponent<BoltAnim>().Init();
            b1.SetActive(true);
            b1.transform.position = ejectLeft.position;
            b1.transform.localEulerAngles = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            b1.transform.localScale = 0.05f * Vector3.one;
            b1.GetComponent<Rigidbody>().velocity = ejectSpeed * ejectLeft.transform.right;

            GameObject b2 = Instantiate(boltPrefab);
            b2.GetComponent<BoltAnim>().Init();
            b2.SetActive(true);
            b2.transform.position = ejectRight.position;
            b2.transform.localEulerAngles = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            b2.transform.localScale = 0.05f * Vector3.one;
            b2.GetComponent<Rigidbody>().velocity = -ejectSpeed * ejectRight.transform.right;
        }
    }
}
