using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UICounter : MonoBehaviour
{
    [SerializeField] Text label;
    [SerializeField] Image[] dots;
    [SerializeField] int value;
    [SerializeField] int maxValue = 5;

    [SerializeField] Color onColor;
    [SerializeField] Color offColor;
    // Start is called before the first frame update
    void Start()
    {
        OnValidate();
    }

    // Update is called once per frame
    void Update()
    {
        for (int d = 0; d < maxValue; ++d)
        {
            dots[d].color = d < value ? onColor : offColor;
        }
    }

    private void OnValidate()
    {
        maxValue = Mathf.Min(maxValue, dots.Length);
        for (int d = 0; d < dots.Length;++d)
        {
            dots[d].gameObject.SetActive(d < maxValue);
            dots[d].color = d < value ? onColor : offColor;
        }
    }
}
