using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour
{
    public GameObject logoText;
    public GameObject logoImage;
    // Start is called before the first frame update
    public void Start()
    {
        logoText.SetActive(false);
        logoImage.SetActive(false);
    }

    // Update is called once per frame
    public void OnMouseOver()
    {
        logoText.SetActive(true);
        logoImage.SetActive(true);
    }

    public void OnMouseExit()
    {
        logoText.SetActive(false);
        logoImage.SetActive(false);
    }
}
