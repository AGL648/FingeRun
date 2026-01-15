using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Velocidad del jugador
    public float moveSpeed;
    //El rigidbody del jugador
    private Rigidbody2D theRB;
    //Fuerza de salto del jugador
    public float jumpForce;
    public float bounceForce;
    private TrailRenderer tr;
    public string startScene;
    public string startScene2;
    //Variable para saber si el jugador est� en el suelo
    private bool isGrounded;
    //Punto por debajo del jugador que tomamos como referencia para detectar el suelo
    public Transform groundCheckPoint;
    //Variable para detectar el Layer de suelo
    public LayerMask whatIsGround;

    //Variables para el contador de tiempo del KnockBack
    public float knockBackLength, knockBackForce; //Valor que tendr� el contador de KnockBack, y la fuerza de KnockBack
    private float knockBackCounter; //Contador de KnockBack

    //Variable para saber si podemos hacer doble salto
    private bool canDoubleJump;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 30f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    //Referencia al Animator del jugador
    private Animator anim;
    //Referencia al SpriteRenderer del jugador
    private SpriteRenderer theSR;

    public bool isLeft;
    public bool isRight;

    //Variable para conocer hacia donde mira el jugador
    

    //Hacemos el Singleton de este script
    public static PlayerController sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el RigidBody del jugador
        theRB = GetComponent<Rigidbody2D>();
        //Rellenamos la referencia del Animator del jugador, porque accedemos a ese componente del jugador usando GetComponent
        anim = GetComponent<Animator>();
        //Rellenamos la referencia del SpriteRenderer del jugador
        theSR = GetComponent<SpriteRenderer>();

        tr = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDashing){
            return;
        }
        //Si el contador de KnockBack se ha vaciado, el jugador recupera el control del movimiento
        if (knockBackCounter <= 0)
        {
            //El jugador se mueve 8 en X, y la velocidad que ya tuviera en Y
            theRB.linearVelocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.linearVelocity.y);

            //La variable isGrounded se har� true siempre que el c�rculo f�sico que hemos creado detecte suelo
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);//OverlapCircle(punto donde se genera el c�rculo, radio del c�rculo, layer a detectar)

            //Si se pulsa el bot�n de salto
            if (Input.GetButtonDown("Jump"))
            {
                //Si el jugador est� en el suelo
                if (isGrounded)
                {
                    //El jugador salta, manteniendo su velocidad en X, y aplicamos la fuerza de salto
                    theRB.linearVelocity = new Vector2(theRB.linearVelocity.x, jumpForce);
                    //Una vez en el suelo, reactivamos la posibilidad de doble salto
                    canDoubleJump = true;
                    Debug.Log("salto");
                }
                //Si el jugador no est� en el suelo
                else
                {
                    //Si la variable booleana canDoubleJump es verdadera
                    if (canDoubleJump)
                    {
                        //El jugador salta, manteniendo su velocidad en X, y aplicamos la fuerza de salto
                        theRB.linearVelocity = new Vector2(theRB.linearVelocity.x, jumpForce);
                        //Hacemos que no se pueda volver a saltar de nuevo
                        canDoubleJump = false;
                    }
                }
            }

            //Girar el sprite del jugador seg�n su direcci�n de movimiento
            //Si el jugador se mueve hacia la izquierda
            if (theRB.linearVelocity.x < 0)
            {
                //No cambiamos la direcci�n del sprite
                theSR.flipX = true;
                //El jugador mira a la izquierda
                isLeft = true;
                isRight = false;
               
            }
            //Si el jugador por el contrario se est� moviendo hacia la derecha
            else if (theRB.linearVelocity.x > 0)
            {
                //Cambiamos la direcci�n del sprite
                theSR.flipX = false;
                //El jugador mira a la derecha
                isLeft = false;
                isRight = true;
                
            }
        }
        //Si el contador de KnockBack todav�a no est� vac�o
        else
        {
            //Hacemos decrecer el contador en 1 cada segundo
            knockBackCounter -= Time.deltaTime;
            //Si el jugador mira a la izquierda
            if (!theSR.flipX)
            {
                //Aplicamos un peque�o empuje a la derecha
                theRB.linearVelocity = new Vector2(knockBackForce, theRB.linearVelocity.y);
            }
            //Si el jugador mira a la derecha
            else
            {
                //Aplicamos un peque�o empuje a la izquierda
                theRB.linearVelocity = new Vector2(-knockBackForce, theRB.linearVelocity.y);
            }
        }

        //ANIMACIONES DEL JUGADOR
        //Cambiamos el valor del par�metro del Animator "moveSpeed", dependiendo del valor en X de la velocidad de Rigidbody
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.linearVelocity.x));//Mathf.Abs hace que un valor negativo sea positivo, lo que nos permite que al movernos a la izquierda tambi�n se anime esta acci�n
        //Cambiamos el valor del par�metro del Animator "isGrounded", dependiendo del valor de la booleana del c�digo "isGrounded"
        anim.SetBool("isGrounded", isGrounded);

        //Dash
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
            theSR.flipX = true;
            isLeft = true;
            isRight = false;
        }

        if(Input.GetKeyDown(KeyCode.RightShift) && canDash)
        {
            StartCoroutine(Dash());
            theSR.flipX = false;
            isLeft = false;
            isRight = true;
        }
    }

    //M�todo para gestionar el KnockBack producido al jugador al hacerse da�o
    public void KnockBack()
    {
        //Inicializar el contador de KnockBack
        knockBackCounter = knockBackLength;
        //Paralizamos en X al jugador y hacemos que salte en Y
        theRB.linearVelocity = new Vector2(0f, knockBackForce);

        //Activamos el trigger del animator
        anim.SetTrigger("hurt");
    }

     public void Bounce()
    {
        //Impulsamos al jugador rebotando
        theRB.linearVelocity = new Vector2(theRB.linearVelocity.x, bounceForce);
        
    }

    private IEnumerator Dash(){
        canDash = false;
        isDashing = true;
        float originalGravity = theRB.gravityScale;
        theRB.gravityScale = 0f;
        
        if (isLeft)
        {
            theRB.linearVelocity = new Vector2(transform.localScale.x * -dashingPower, 0f);
        }
        else
        {
            theRB.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        }
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        theRB.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            SceneManager.LoadScene(startScene);
        }

         if (collision.gameObject.tag == "Virus")
        {
            SceneManager.LoadScene(startScene2);
        }
    }

    
}
