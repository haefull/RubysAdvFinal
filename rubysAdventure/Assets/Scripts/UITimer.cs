using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITimer : MonoBehaviour
{
    public static UITimer instance { get; private set; }

    TextMeshProUGUI textmeshpro;
    
    void Awake()
    {
        instance = this;
        textmeshpro = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
    }

    public void UpdateTimer(float time)
    {
        textmeshpro.text = time.ToString("0.00");
    }
}
