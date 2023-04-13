using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumScript : MonoBehaviour
{
    public int direction;//0 sur, 1 este, 2 oeste, 3 norte

    public enum Direction { North, East, South, West};
    public Direction myDirection;

    void Start()
    {
        if(myDirection == Direction.North)
        {
            myDirection = Direction.West;
        }
        //Para coger el valor int del enumerado
        //Debug.Log((int)myDirection);
    }
    void Update()
    {
        switch(direction)
        {
            case 1:
                Debug.Log("Direction value: " + direction);
                break;
            case 5:
                direction++;
                Debug.Log("Direction value: " + direction);
                break;
            case 3:
                direction--;
                Debug.Log("Direction value: " + direction);
                break;
        }

        switch(myDirection)
        {
            case Direction.North:
                Debug.Log("North");
                break;
            case Direction.West:
                Debug.Log("West");
                break;
        }
    }
}
