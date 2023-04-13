using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    [Range(0,800)]
    public float currentHealth;

    public Image[] heartsUI;//los corazones que tengo en la interfaz

    public bool death;
    public bool damaged;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        HeartsUI();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack"))
        {
            TakeDamage(10);
        }
    }
    void TakeDamage(int amount)
    {
        if (death || damaged) return;

        damaged = true;
        currentHealth -= amount;
        //gestión de corazones en la interfaz

        if (currentHealth <= 0) Death();
        else anim.SetTrigger("Hit");
    }
    //función que vamos a llamar como evento en la animación de daño, al final, para volver a poner
    //el daño a false
    void DamagedToFalse() => damaged = false;
    void Death()
    {
        death = true;
        anim.SetTrigger("Death");
        Destroy(gameObject, 2);
    }

    void HeartsUI()
    {
        if (currentHealth == maxHealth) return;

        float x = currentHealth / maxHealth;
        float y = heartsUI.Length * x;//y es un número decimal donde la parte entera representa
        //el nº de corazones que tengo que llenar totalmente y la parte decimal representa cuánto tengo que
        //llenar del último corazón

        float dec = y % 1;//numero float % 1 devuelve la parte decimal de ese número float
        float num = (int)y;//para sacar la parte entera, convierto a int el número float

        for(int i=0; i < heartsUI.Length; i++)
        {
            if(i<=num)
            {
                if (i == num) heartsUI[i].fillAmount = dec;
                else heartsUI[i].fillAmount = 1;
            }
            else heartsUI[i].fillAmount = 0;
        }
    }
}
