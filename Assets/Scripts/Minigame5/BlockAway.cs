using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAway : MonoBehaviour
{
    private Animator Player;
    private Rigidbody2D rb;
    public Vector2 direccionDeGolpeo;
    public float NuevaVelocidadDeGolpeo;
    public Animator anim;
    public AudioSource PersonaDaño;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player = collision.gameObject.GetComponent<Animator>();

            if (Player != null && Player.GetCurrentAnimatorStateInfo(0).IsName("Block"))
            {
                rb.velocity = direccionDeGolpeo.normalized * NuevaVelocidadDeGolpeo;
                anim.SetTrigger("Death");
                PersonaDaño.Play();
            }
        }


    }
}
