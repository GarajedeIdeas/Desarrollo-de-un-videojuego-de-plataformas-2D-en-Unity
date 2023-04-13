using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            GameManager.gameManager.AddCoins();
            Destroy(collision.gameObject);
        }
    }  
}
