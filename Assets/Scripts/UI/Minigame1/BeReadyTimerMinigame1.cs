using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BeReadyTimerMinigame1 : MonoBehaviour
{

    [SerializeField] private float tiempoMaximo;

    [SerializeField] private Slider slider;

    private float tiempoActual;

    private bool tiempoActivado = false; 

    public string startScene;

    private void Start()
    {
        ActivarTemporizador();
    }

    private void Update()
    {

        if(tiempoActivado)
        {
            CambiarContador();
        } 
        if(Input.GetKeyDown(KeyCode.KeypadEnter)){
            SceneManager.LoadScene(startScene);
        }

    }

    private void CambiarContador()
    {
        tiempoActual -= Time.deltaTime;

        if(tiempoActual >= 0){
            slider.value = tiempoActual;
        }

        if(tiempoActual <= 0)
        {
            SceneManager.LoadScene(startScene);
        }
    }

    private void CambiarTemporizador(bool estado){
        tiempoActivado = estado;
    }

    public void ActivarTemporizador()
    {
        tiempoActual = tiempoMaximo;
        slider.maxValue = tiempoMaximo;
        CambiarTemporizador(true);
    }

    public void DesactivarTemporizador(){
        CambiarTemporizador(false);
    }

   
}
