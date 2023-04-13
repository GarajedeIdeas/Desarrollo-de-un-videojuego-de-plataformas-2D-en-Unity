using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 pos2;
    public int speed;

    Vector3 posToGo;

    private void Start()
    {
        posToGo = pos1;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, posToGo, speed * Time.deltaTime);

        if(transform.position == posToGo)
        {
            if (posToGo == pos1) posToGo = pos2;
            else posToGo = pos1;
        }
    }
}
