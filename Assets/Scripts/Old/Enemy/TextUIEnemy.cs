using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUIEnemy : MonoBehaviour
{
    public int speed;
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
