using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    public static UIGameOver instance { get; private set; }
    
    void Awake()
    {
        instance = this;
        var textmeshpro = GetComponent<TextMeshProUGUI>();
        textmeshpro.text = "";
    }

    void Start()
    {
    }

    public void WinGame()
    {
        var textmeshpro = GetComponent<TextMeshProUGUI>();
        textmeshpro.text = "You Win! Game created by Group 2.";
    }

    public void LoseGame()
    {
        var textmeshpro = GetComponent<TextMeshProUGUI>();
        textmeshpro.text = "You Lost! Press R to restart!";
    }
}