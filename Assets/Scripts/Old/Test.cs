using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //Declaración/creación de la lista (enteros)
    List<int> numbers = new List<int>();

    List<GameObject> myObjects = new List<GameObject>();

    Dictionary<GameObject, int> cards = new Dictionary<GameObject, int>();

    public GameObject _object;

    private void Start()
    {

        cards.Add(this.gameObject, 3);
        cards.Add(_object, 5);
        Debug.Log("Dictionary Count " + cards.Count);

        foreach(var i in cards)
        {
            Debug.Log(i.Key.name);
            Debug.Log(i.Value);
           
        }

        cards[_object] = 8;
        foreach (var i in cards)
        {
            Debug.Log(i.Key.name);
            Debug.Log(i.Value);

        }


        /* numbers.Add(3);
         numbers.Add(5);
         numbers.Add(6);

         Debug.Log("Longitud de la lista: " + numbers.Count);
         for(int i=0; i<numbers.Count;i++)
         {
             Debug.Log("" + numbers[i]);
         }

         numbers.RemoveAt(1);

         for (int i = 0; i < numbers.Count; i++)
         {
             Debug.Log("" + numbers[i]);
         }*/
    }

    void Update()
    {
        //Debug.Log("Global position: " + transform.position);
        //Debug.Log("Local position: " + transform.localPosition);
    }
}
