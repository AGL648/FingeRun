using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class ScrollingBackground : MonoBehaviour
{
    private Image Imagen;
    [SerializeField] private Vector2 velocidad;

    void Start()
    {
        Imagen = GetComponent<Image>();
        Imagen.material = new Material(Imagen.material);
    }

    void Update()
    {
        Imagen.material.mainTextureOffset += velocidad * Time.deltaTime;
    }
}
