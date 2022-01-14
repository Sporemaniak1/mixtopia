using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Script : MonoBehaviour
{



    public Rigidbody2D RB;
    public BoxCollider2D Colider;
    [SerializeField] private int MaxHP;
    public int currentHealth;
    public bool Destroyed;
    private Animator anim;

    private void Start()
    {
        Destroyed = false;
        currentHealth = MaxHP;
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        if (currentHealth <= 0) { Destroyed = true; }


        if (Destroyed)
        {
            anim.SetBool("Destroyed", Destroyed);
            RB.isKinematic = true;
            Colider.enabled = false;
        }

    }

    public void Damage()
    {
        currentHealth = currentHealth--;
    }

   

}
