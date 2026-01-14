using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BajarVolumenMúsica : MonoBehaviour
{
    [SerializeField] AudioSource music;

    // Start is called before the first frame update
    public void OnMusic()
    {
        music.Play();
    }

    // Update is called once per frame
    public void OffMusic()
    {
        music.Stop();
    }
}
