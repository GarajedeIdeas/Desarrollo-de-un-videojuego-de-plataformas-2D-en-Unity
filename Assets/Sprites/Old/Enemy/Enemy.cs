using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Patrol")]
    public int speed;
    public Vector2 leftLimit;
    public Vector2 rightLimit;

    [Header("Following - Attack")]
    public float distanceToFollowingPlayer;//la distancia a la que quiero que deje de hacer la patrulla y persiga al player
    public float distanceToAttackPlayer;//distancia a la que tiene que estar el enemigo del player para poder atacar
    public float xFactor;//stopping distance del enemigo hacia el player, para que no "se meta" dentro del player
    //y se queda a una cierta distancia

    [Header("Colliders Attack")]
    public GameObject colliderAttackRight;
    public GameObject colliderAttackLeft;

    public enum State {Patrol, Following, Attacking};//Creación del enumerado
    public State stateEnemy;//Creación de la variable 

    Animator anim;
    SpriteRenderer spriteRenderer;
    EnemyHealth enemyHealth;

    Vector2 posToGo;//variable donde voy a guardar en todo momento hacia donde quiero que se diriga el enemigo
    GameObject player;
    float distanceToPlayer;//variable donde voy a recoger en todo momento la distancia que hay entre player y enemigo
    bool followingPlayer;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //Le damos un valor inicial a posToGo
        //El player no se va a mover a lo largo del eje Y, por lo tanto le digo el postogo que no se mueva
        //en su eje Y
        posToGo = new Vector2(rightLimit.x, transform.position.y);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (enemyHealth.damaged || enemyHealth.death || player == null) return;

        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        switch(stateEnemy)
        {
            case State.Patrol:
                {
                    if (distanceToPlayer <= distanceToFollowingPlayer) stateEnemy = State.Following;
                    Patrol();
                    break;
                }

            case State.Following:
                {

                    if (distanceToPlayer > distanceToFollowingPlayer) stateEnemy = State.Patrol;            
                    else if (distanceToPlayer <= distanceToAttackPlayer) stateEnemy = State.Attacking;
                    FollowingPlayer();
                    break;
                }
            case State.Attacking:
                {
                    if (distanceToPlayer > distanceToFollowingPlayer) stateEnemy = State.Patrol;
                    else if (distanceToPlayer > distanceToAttackPlayer) stateEnemy = State.Following;
                    Attack();
                    break;
                }
        }
        Flip();
        Animating();
    }
    void Patrol()
    {
        followingPlayer = false;
        if(transform.position.x == posToGo.x)
        {
            //si iba hacia la derecha, ahora quiero ir hacia la izquierda
            if (posToGo.x == rightLimit.x) posToGo = new Vector2(leftLimit.x, transform.position.y);
            //si iba hacia la izquierda, ahora quiero ir hacia la derecha
            else posToGo = new Vector2(rightLimit.x, transform.position.y);
        }
        transform.position = Vector2.MoveTowards(transform.position, posToGo, speed * Time.deltaTime);
    }
    void FollowingPlayer()
    {
        followingPlayer = true;

        float direction;
        if (player.transform.position.x > transform.position.x) direction = -1;
        else direction = 1;

        posToGo = new Vector2(player.transform.position.x + (xFactor * direction), transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, posToGo, speed * Time.deltaTime);
    }
    void Attack()
    {
        anim.SetBool("Attack", true);
    }
    void Flip()
    {
        Vector3 target;
        if (followingPlayer) target = player.transform.position;
        else target = posToGo;

        if (target.x > transform.position.x) spriteRenderer.flipX = false;
        else if (target.x < transform.position.x) spriteRenderer.flipX = true;
    }
    void Animating()
    {
        if (transform.position.x == posToGo.x) anim.SetBool("IsWalking", false);
        else anim.SetBool("IsWalking", true);

        if (stateEnemy != State.Attacking) anim.SetBool("Attack", false);
    }
    //Esta función la vamos a llamar como evento en la animación de ataque
    void EnableCollider()
    {
        if (spriteRenderer.flipX) colliderAttackLeft.SetActive(true);
        else if (!spriteRenderer.flipX) colliderAttackRight.SetActive(true);

        Invoke("DisableCollider", 0.1f);
    }
    void DisableCollider()
    {
        colliderAttackRight.SetActive(false);
        colliderAttackLeft.SetActive(false);
    }
}
