using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public float vida;

    // Update is called once per frame
    void Update()
    {
        if(vida <= 0)
        {
            print("Has perdido");
        }
    }
}
