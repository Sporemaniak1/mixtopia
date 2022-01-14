using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public float KickRange=0.5f;
    public int KickDmg=5;
    public LayerMask Damagable;
    public Transform KickPoint;

   
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) { Kick(); } ;
    }


    void Kick() 
    {
        animator.SetTrigger("Kick");
        Collider2D[] EnemiesHit = Physics2D.OverlapCircleAll(KickPoint.position, KickRange, Damagable);

        foreach(Collider2D kicked in EnemiesHit) 
        {
            
            kicked.GetComponent<Dummy_Script>().Damage(KickDmg);
            kicked.GetComponent<Box_Script>().Destroyed = true;
            Debug.Log(kicked);
        }
    }

    public void FinishKick()
    {
        animator.SetBool("Kick", false);

    }
}
