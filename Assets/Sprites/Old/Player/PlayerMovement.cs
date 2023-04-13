using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Velocity")]
    public int speed;
    public float acceleration;

    [Header("Attack")]
    public GameObject colliderAttackRight;//Hace referencia al gameobject que tiene como hijo el player que contiene el
    //collider de ataque
    public GameObject colliderAttackLeft;

    [Header("Raycast - Jump")]
    public Transform groundCheck;//un objeto vacío hijo del player que se pone a los pies y que va a representar
    //el origen del raycast
    public float rayLenght;//longitud del raycast
    public LayerMask groundMask;//la capa donde va a estar el suelo
    public bool isGrounded;
    public float jumpForce;

    Animator anim;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;

    bool jumpPressed;//para saber si el botón de salto se ha pulsado y, efectivamente, puedo saltar
    bool isAttacking;//booleana para saber si el player está atacando o no

    Vector2 targetVelocity;//variable donde voy a guardar a la velocidad a la que quiero que se mueva el personaje
    Vector2 dampVelocity;//variable para usar en el SmoothDamp, va a guardar la velocidad actual que lleva el personaje

    float h;//recoge el input horizontal

    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (playerHealth.damaged || playerHealth.death)
        {
            targetVelocity = Vector2.zero;
            return;
        }

        Movement();
        GroundDetection();
        JumpPressed();
        Attack();
        Animating();
        Flip();
    }
    private void FixedUpdate()
    {
        rb2d.velocity = Vector2.SmoothDamp(rb2d.velocity, targetVelocity, ref dampVelocity, acceleration);
        Jump();
    }
    void Movement()
    {
        if(!isAttacking)//no estoy atacando
        {
            h = Input.GetAxis("Horizontal");
            targetVelocity = new Vector2(h * speed, rb2d.velocity.y);
        }
        else//estoy atacando
        {
            targetVelocity = Vector2.zero;
            h = 0;
        }
        
    }
    void Animating()
    {
        if (h != 0) anim.SetBool("IsRunning", true);
        else anim.SetBool("IsRunning", false);

        anim.SetFloat("VelocityY", rb2d.velocity.y);
        anim.SetBool("IsJumping", !isGrounded);//el parámetro IsJumping va a coger el valor contrario al que
        //tenga la variable isGrounded
    }
    void Flip()
    {
        //si el personaje se está moviendo hacia la derecha
        if (h > 0) spriteRenderer.flipX = false;
        //si el personaje se está moviendo hacia la izquierda
        else if (h < 0) spriteRenderer.flipX = true;
    }
    void GroundDetection()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, rayLenght, groundMask);
        Debug.DrawRay(groundCheck.position, Vector2.down * rayLenght, Color.red);
    }
    #region Attack
    void Attack()
    {
        if(Input.GetMouseButtonDown(0) && isGrounded)
        {
            isAttacking = true;
            rb2d.velocity = Vector2.zero;//reseteo su velocidad

            anim.SetTrigger("Attack");//Activo el parámetro trigger Attack y pasamos de idle a la submáquina
            //de estados PlayerAttack
            int n = Random.Range(0, 3);//el 3 no lo devuelve
            anim.SetInteger("AttackSelector", n);
        }
    }
    //Función que vamos a llamar como evento en las animaciones para poner el booleano a falso
    void AttackToFalse()
    {
        isAttacking = false;
    }
    //Función que vamos a llamar desde la animación de ataque JUSTO EN EL KEYFRAME QUE QUIERO ATACAR
    void EnableCollider()
    {
        if(!spriteRenderer.flipX)//si estoy mirando a la derecha
            colliderAttackRight.SetActive(true);
        else//si estoy mirando a la izquierda
            colliderAttackLeft.SetActive(true);
        Invoke("DisableCollider", 0.1f);
    }
    void DisableCollider()
    {
        colliderAttackRight.SetActive(false);
        colliderAttackLeft.SetActive(false);
    }
    #endregion

    #region Jump
    /// <summary>
    /// Función para saber si el usuario puede saltar o no
    /// </summary>
    void JumpPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isAttacking)
            jumpPressed = true;
    }
    void Jump()
    {
        if(jumpPressed)
        {
            jumpPressed = false;
            rb2d.AddForce(Vector2.up * jumpForce);
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Platform"))
        {
            //pongo al player como hijo de la pataforma
            transform.SetParent(collision.transform);
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            //cuando deja de estar sobre la plataforma, le pongo al player el padre a null
            transform.SetParent(null);
        }

    }

}
