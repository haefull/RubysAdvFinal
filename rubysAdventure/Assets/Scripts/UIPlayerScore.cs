using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerScore : MonoBehaviour
{
    public static UIPlayerScore instance { get; private set; }
    
    void Awake()
    {
        instance = this;
        var textmeshpro = GetComponent<TextMeshProUGUI>();
        textmeshpro.text = "Fixed Robots: " + 0;
    }

    void Start()
    {
    }

    public void SetValue(int value)
    {				  
        var textmeshpro = GetComponent<TextMeshProUGUI>();
        textmeshpro.text = "Fixed Robots: " + value.ToString();
        //mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}