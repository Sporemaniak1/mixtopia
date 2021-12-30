using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Script : MonoBehaviour
{
    
    [SerializeField] private float MaxHP;

    public float currentHealth;
    public bool dying;
    private Animator anim;

    private void Start()
    {
        dying = false;
        currentHealth = MaxHP;
        anim=GetComponent<Animator>();
       
    }

    private void Update()
    {
        if (currentHealth < 0) { Dying(); }
    }

    private void Damage(float ammout) 
    {
        currentHealth -= ammout;
    }

    private void Dying() 
    {
        if (!dying) 
        {
            dying = true;
            anim.SetBool("Dying", dying);

        }
    }
    private void Die() 
    {
        anim.SetBool("Dead", true);
        anim.SetBool("Alive", false);
    }

}
