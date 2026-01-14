using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEvent : MonoBehaviour
{
    public AudioSource ThreeTwoOne;

    public void PlaySound()
    {
        ThreeTwoOne.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
