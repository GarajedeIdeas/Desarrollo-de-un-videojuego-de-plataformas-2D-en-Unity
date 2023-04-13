using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int direction;
    public float minForceValue;
    public float maxForceValue;
    public float torqueValue;

    Rigidbody2D rb2d;
    float forceValue;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //Random.value devuelve un valor entre 0 y 1
        if (Random.value < 0.5f) direction = 1;
        else direction = -1;

        forceValue = Random.Range(minForceValue, maxForceValue);

        rb2d.AddForce(Vector2.up * forceValue * 1.5f);
        rb2d.AddForce(Vector2.right * forceValue/2 * direction);
        rb2d.AddTorque(torqueValue * direction);
    }


}
