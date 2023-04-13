using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    [Header ("Health")]
    public float maxHealth;
    public float currentHealth;
    public Image lifeUI;//la barra de vida que tiene en la cabeza el enemigo

    [Header ("Damage - Death")]
    public TextMeshProUGUI textUI;
    public bool damaged;
    public bool death;
    public AnimationClip clipHit;//la animaci�n de da�o del enemigo, porque voy a querer saber su duraci�n
    public float timeToShowText;//tiempo que voy a mostrar el texto de da�o sobre el enemigo
    public GameObject coinPrefab;
    public GameObject heartPrefab;
    public int numCoinsMax;

    Animator anim;
    Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        lifeUI.fillAmount = 1;
        textUI.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerAttack"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        if (death == true) return;//si el enemigo est� muerto, dejo de ejecutar la funci�n

        damaged = true;
        Invoke("DamagedToFalse", clipHit.length);

        currentHealth -= 10;
        lifeUI.fillAmount = currentHealth / maxHealth;

        StopCoroutine("TextDamageUI");
        StartCoroutine("TextDamageUI");

        if (currentHealth <= 0) Death();
        else if (enemy.stateEnemy != Enemy.State.Attacking) anim.SetTrigger("Hit");//Activo el par�metro del animator para reproducir la animaci�n de Hit

    }
    void Death()
    {
        death = true;
        anim.SetTrigger("Death");
        Dropping();
        Destroy(gameObject, 2);
    }
    void Dropping()
    {
        int n = Random.Range(1, numCoinsMax);
        for(int i=0; i <= n; i++)
        {
            //hay un 10% de posibilidades de que instancie un coraz�n
            if(Random.value <= 0.1f) Instantiate(heartPrefab, transform.position, new Quaternion());
            else Instantiate(coinPrefab, transform.position, new Quaternion());//new Quaternion hace referencia a rotaci�n 0,0,0
        }
    }
    void DamagedToFalse()
    {
        damaged = false;
    }
    IEnumerator TextDamageUI()
    {
        textUI.gameObject.SetActive(true);
        textUI.transform.localPosition = Vector3.zero;
        textUI.text = "10";
        yield return new WaitForSeconds(timeToShowText);
        textUI.gameObject.SetActive(false);
    }

}
