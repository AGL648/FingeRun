using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class FillValueNumber : MonoBehaviour
{
    public Image TargetImage;

    public UnityEngine.UI.Text gameObject;
    
    // Update is called once per frame
    void Update()
    {
        float amount = TargetImage.fillAmount * 100;
        gameObject.text = amount.ToString("F0");
    }
}
