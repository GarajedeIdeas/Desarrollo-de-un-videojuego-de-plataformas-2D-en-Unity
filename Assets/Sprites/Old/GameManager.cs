using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public int numCoins;
    public TextMeshProUGUI textCoinsUI;

    void Awake()
    {
        gameManager = this;
    }

    void Update()
    {
        
    }

    public void AddCoins()
    {
        numCoins++;
        textCoinsUI.text = numCoins.ToString();
    }
}
