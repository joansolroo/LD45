using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class UIBar : MonoBehaviour
{
    [SerializeField] public Text label;
    [SerializeField] public Image background;
    [SerializeField] public Image shadow;
    [SerializeField] public Image foreground;

    [SerializeField] float backDelay = 1;
    [SerializeField] public float value;
    Vector2 hpBarMaxSize;
    Vector2 hpBarCurrentSize;
    Vector2 hpBarCurrentSizeSmooth;

    public string Label { get { return label.text; } set { label.text = value; } }
    // Start is called before the first frame update
    void Start()
    {
        OnValidate();
    }

    // Update is called once per frame
    void Update()
    {
        hpBarCurrentSize.x = value;
        foreground.rectTransform.localScale = hpBarCurrentSize;
        hpBarCurrentSizeSmooth.x = Mathf.MoveTowards(hpBarCurrentSizeSmooth.x, hpBarCurrentSize.x, Time.deltaTime * backDelay);
        shadow.rectTransform.localScale = hpBarCurrentSizeSmooth;
    }

    private void OnValidate()
    {
        hpBarMaxSize = foreground.rectTransform.localScale;
        hpBarCurrentSize = hpBarMaxSize;
        hpBarCurrentSizeSmooth = hpBarMaxSize;

        hpBarCurrentSize.x = value;
        foreground.rectTransform.localScale = hpBarCurrentSize;
        shadow.rectTransform.localScale = hpBarCurrentSize;
    }
}
