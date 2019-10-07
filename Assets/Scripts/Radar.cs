using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] Sense sense;

    [SerializeField] int maxSight;
    [SerializeField] RadarArrow ArrowPrefab;
    RadarArrow[] arrows;
    [SerializeField] Transform swipe;

    [SerializeField] float minDistance = 0;
    [SerializeField] float maxDistance = 5;
    // Start is called before the first frame update
    void Start()
    {
        arrows = new RadarArrow[maxSight];
        for(int a = 0; a < maxSight;++a)
        {
            RadarArrow arrow = GameElement.Instantiate<RadarArrow>(ArrowPrefab);
            arrows[a] = arrow;
            arrow.transform.parent = this.transform;
            arrow.transform.localPosition = Vector3.zero;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int c = 0;
        foreach(GameObject p in sense.perceived.Keys)
        {
            if(c < maxSight)
            {
                if (p != null)
                {
                    arrows[c].gameObject.SetActive(true);
                    arrows[c].transform.LookAt(p.transform);
                    float d = Vector3.Distance(this.transform.position, p.transform.position);
                    arrows[c].SetVisibility(1-(d/2 - minDistance) / (maxDistance - minDistance));
                    ++c;
                }
            }
        }
        for(;c<maxSight;++c)
        {
            arrows[c].gameObject.SetActive(false);
        }
        swipe.Rotate(0, 0,60 * Time.deltaTime);
    }
}
