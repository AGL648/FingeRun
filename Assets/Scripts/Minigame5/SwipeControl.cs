using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SwipeControl : MonoBehaviour
{
    float direction = 1;
    public float velocidad;
    public float salto;
    private Rigidbody2D playerRB;
    bool MirandoDerecha = true;

    public float TiempoMaxSwipe;
    public float DistanciaMinSwipe;

    private float EmpiezaSwipe;
    private float TerminaSwipe;
    private float TiempoSwipe;

    private Vector2 CambiarPosicionSwipe;
    private Vector2 EstablecerPosicionSwipe;
    private float LongitudSwipe;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerRB.linearVelocity = new Vector2(direction * velocidad * Time.deltaTime, playerRB.linearVelocity.y);
        TestSwipe();
        ControlSwipe();

    }

    void TestSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                EmpiezaSwipe = Time.time;
                CambiarPosicionSwipe = touch.position;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                TerminaSwipe = Time.time;
                EstablecerPosicionSwipe = touch.position;
                TiempoSwipe = TerminaSwipe - EmpiezaSwipe;
                LongitudSwipe = (EstablecerPosicionSwipe - CambiarPosicionSwipe).magnitude;
                if(TiempoSwipe<TiempoMaxSwipe && LongitudSwipe > DistanciaMinSwipe)
                {
                    ControlSwipe();
                }
            }
        }
    }

    void ControlSwipe()
    {
        Vector2 Distancia = EstablecerPosicionSwipe - CambiarPosicionSwipe;
        float xDistancia = Mathf.Abs(Distancia.x);
        float yDistancia = Mathf.Abs(Distancia.y);
        if(xDistancia > yDistancia)
        {
            if(Distancia.x>0 && !MirandoDerecha)
            {
                //ANIMACION
            }

            if (Distancia.x<0 && MirandoDerecha)
            {
                //ANIMACION
            }
        }

        else if(yDistancia > xDistancia)
        {
            if (Distancia.y > 0)
            {
                playerRB.linearVelocity = Vector2.up * salto * Time.deltaTime;
            }
            else if(Distancia.y < 0)
            {
                Debug.Log("DeslizaALaDerecha");
            }
        }
    }
}
