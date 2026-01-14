using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandScript7 : MonoBehaviour
{
	Vector2 startTouchPosition, endTouchPosition;
	Rigidbody2D rb;
	[SerializeField] bool isGrounded = false;
	[SerializeField] bool Damaged = true;
	public float life;
	public UnityEngine.UI.Text ScoreTxt;
	public UnityEngine.UI.Text LifeTxt;
	public Animator anim;
	bool isAlive = true;
	public string startScene;
	public string startScene2;
	public float score;
	public float jumpForce;
	bool activate = false;
	public AudioSource Salto;
	public AudioSource Deslizar;
	public AudioSource Golpe;
	public AudioSource Bloqueo;
	public AudioSource Daño;



	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		//Salto = GetComponent

	}

	void Update()
	{

		anim.SetBool("IsGrounded", isGrounded);
		SwipeCheck();

		if (isAlive)
		{
			score += Time.deltaTime * 5;
			ScoreTxt.text = "Distancia : " + score.ToString("F") + " m";
			Time.timeScale = 1f;
		}
		if (score > 900)
		{
			SceneManager.LoadScene(startScene);
		}

	}

	private void SwipeCheck()
	{

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
			startTouchPosition = Input.GetTouch(0).position;

		}			

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			endTouchPosition = Input.GetTouch(0).position;
			activate = false;
			float distanciatotalenY = Mathf.Abs(startTouchPosition.y - endTouchPosition.y);
			float distanciatotalenX = Mathf.Abs(startTouchPosition.x - endTouchPosition.x);

			if (distanciatotalenY > distanciatotalenX)
			{

				if (endTouchPosition.y > startTouchPosition.y && rb.velocity.y == 0)
				{
					JumpIfAllowed();
				}

				if (endTouchPosition.y < startTouchPosition.y && rb.velocity.y == 0)
				{
					CrouchIfAllowed();
				}

			}
			else
			{

				if (endTouchPosition.x > startTouchPosition.x && rb.velocity.x == 0)
				{
					AttackIfAllowed();
				}

				if (endTouchPosition.x < startTouchPosition.x && rb.velocity.x == 0)
				{
					BlockIfAllowed();
				}
			}
		}



	}

	void JumpIfAllowed()
	{
		if (isGrounded == true)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
			//rb.AddForce(Vector2.up * jumpForce);
			anim.SetTrigger("Jump");
			Salto.Play();

		}
	}

	void CrouchIfAllowed()
	{
	
			rb.AddForce(Vector2.down);
			
			anim.SetTrigger("Crouch");
			Deslizar.Play();


	}

	void AttackIfAllowed()
	{

			anim.SetTrigger("Attack");
			Golpe.Play();

	}

	void BlockIfAllowed()
	{

			anim.SetTrigger("Block");
			Bloqueo.Play();

	}

	/*void StompIfAllowed()
	{

			rb.AddForce(Vector2.down * jumpForce);

			anim.SetTrigger("Stomp");


	}*/

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
		
				isGrounded = true;
				Damaged = true;

			
        }       

		if (collision.gameObject.CompareTag("Spike") && life > 0)
		{
			life -= 1;
			LifeTxt.text = "" + life;
			anim.SetBool("Hurt", true);
			Damaged = true;
			Daño.Play();
		}


		if (collision.gameObject.CompareTag("Stink") && life > 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
		{
			life -= 1;
			LifeTxt.text = "" + life;
			anim.SetBool("Hurt", true);
			Damaged = true;
			Daño.Play();
		}

		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && isGrounded == false && collision.gameObject.CompareTag("Spike"))
		{
			life -= 1;
			LifeTxt.text = "" + life;
			anim.SetBool("Hurt", true);
			Damaged = true;
			Daño.Play();
		}


		if (collision.gameObject.CompareTag("Block") && life > 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Block"))
		{
			life -= 1;
			LifeTxt.text = "" + life;
			anim.SetBool("Hurt", true);
			Daño.Play();
		}

		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
			isGrounded = false;
        }

		else if (life <= 0)
		{
			isAlive = false;
			Time.timeScale = 1;
			print("Has perdido");
			anim.SetTrigger("Hurt");
			SceneManager.LoadScene(startScene2);
			Daño.Play();
		}
		else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
		{
			Damaged = true;
			isGrounded = true;
			Daño.Play();
		}
	}
    public void OnCollisionExit2D(Collision2D collision)
    {

		if (collision.gameObject.CompareTag("Ground"))
		{
			
				isGrounded = false;
				

		}

	}
    
}
