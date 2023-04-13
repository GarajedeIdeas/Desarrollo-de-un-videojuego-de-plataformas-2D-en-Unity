using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed;
    public float acceleration;

    float h;
    Animator anim;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2D;

    Vector2 targetVelocity;//la velocidad que quiero que alcanze el player
    Vector2 dampVelocity;

    void Start()
    {
        //Hace la referencia al componente Animator
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        //transform.Translate(Vector2.right * speed * h * Time.deltaTime);
        InputPlayer();
        Animating();
        Movement();
        Flip();
    }
    private void FixedUpdate()
    {
        rb2D.velocity = Vector2.SmoothDamp(rb2D.velocity, targetVelocity, ref dampVelocity,
            acceleration);
    }
    void Movement()
    {
        targetVelocity = new Vector2(h * speed, rb2D.velocity.y);
    }
    void InputPlayer()
    {
        h = Input.GetAxis("Horizontal");//D->1 A->-1, 0 si no se pulsa tecla
    }
    void Animating()
    {
        if (h != 0)
            anim.SetBool("IsRunning", true);
        else
            anim.SetBool("IsRunning", false);
    }
    void Flip()
    {
        //Vamos hacia la derecha
        if (h > 0)
            spriteRenderer.flipX = false;
        else if (h < 0)//vamos hacia la izquierda
            spriteRenderer.flipX = true;
    }
}
