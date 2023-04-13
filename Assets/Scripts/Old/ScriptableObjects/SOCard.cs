using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class SOCard : ScriptableObject
{
    public string nameCard;
    public Sprite imageCard;
    public int health;
    public float force; 

    public void ShowNameOnConsole()
    {
        Debug.Log("NameCard: " + nameCard);
    }
}
