using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Velocidad del enemigo
    public float moveSpeed;

    //Posiciones m�s a la izquierda y m�s a la derecha que se va a poder mover el enemigo
    public Transform leftPoint, rightPoint;

    //Variable para conocer la direcci�n de movimiento del enemigo
    private bool movingRight;

    //Referencia al RigidBody del enemigo
    private Rigidbody2D theRB;
    //Referencia al SpriteRenderer del enemigo
    private SpriteRenderer theSR;

    [SerializeField] private float vida;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el Rigidbody del enemigo
        theRB = GetComponent<Rigidbody2D>();
        //Inicializamos el SpriteRenderer del enemigo teniendo en cuenta que est� en el GO hijo
        theSR = GetComponentInChildren<SpriteRenderer>();

        //Sacamos el Leftpoint y el Rightpoint del objeto padre, para que no se muevan junto con este
        leftPoint.parent = null;//null es vac�o o no tiene en este caso
        rightPoint.parent = null;

        animator = GetComponent<Animator>();
    }

    public void TomarDano(float dano){
        vida -= dano;
        if(vida <= 0){
            GetComponentInParent<EnemyDeath>().EnemyDeathController();
            Muerte();
        }
    }

    private void Muerte(){

    }

    // Update is called once per frame
    void Update()
    {
        //Si el enemigo se est� moviendo hacia la derecha
        if (movingRight)
        {
            //Aplicamos una velocidad hacia la derecha al enemigo
            theRB.linearVelocity = new Vector2(moveSpeed, theRB.linearVelocity.y);

            //Giramos en horizontal el sprite del enemigo
            theSR.flipX = true;

            //Si la posici�n en X del enemigo est� m�s a la derecha que el RighPoint
            if (transform.position.x > rightPoint.position.x)
            {
                //Ya no se mover� a la derecha sino a la izquierda
                movingRight = false;
            }
        }
        //Si el enemigo se est� moviendo hacia la izquierda
        else
        {
            //Aplicamos una velocidad hacia la izquierda al enemigo
            theRB.linearVelocity = new Vector2(-moveSpeed, theRB.linearVelocity.y);

            //Mantenemos la direcci�n hacia la que mira el sprite
            theSR.flipX = false;

            //Si la posici�n en X del enemigo est� m�s a la izquierda que el LeftPoint
            if (transform.position.x < leftPoint.position.x)
            {
                //Ya no se mover� a la izquierda sino a la derecha
                movingRight = true;
            }
        }
    }
}
