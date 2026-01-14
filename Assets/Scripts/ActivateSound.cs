using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSound : MonoBehaviour
{
    public AudioSource GolpePersona;

    public void PlaySound()
    {
        GolpePersona.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
