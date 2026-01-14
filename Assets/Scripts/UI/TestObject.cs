using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestObject : MonoBehaviour
{
    public bool isOver = false; 
    // Start is called before the first frame update
    public void OnPointEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse on");
        isOver = true;
    }

    // Update is called once per frame
    public void OnPointExit(PointerEventData eventData)
    {
        Debug.Log("Mouse off");
        isOver = false;
    }
}
