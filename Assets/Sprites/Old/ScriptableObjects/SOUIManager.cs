using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SOUIManager : MonoBehaviour
{
    public SOCard card;

    public Image imageUI;
    public TextMeshProUGUI nameCardUI;
    public TextMeshProUGUI healthCardUI;
    public TextMeshProUGUI forceCardUI;

    void Start()
    {
        imageUI.sprite = card.imageCard;
        imageUI.preserveAspect = true;//para conservar el aspect ratio de la imagen

        nameCardUI.text = card.nameCard;
        healthCardUI.text = card.health.ToString();
        forceCardUI.text = card.force.ToString();

        card.ShowNameOnConsole();
    }
}
