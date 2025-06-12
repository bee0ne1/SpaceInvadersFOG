using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour, ICharacterStats
{
    public int life { get; set; }
    public bool dead { get; set; }
    
    private Animator animator;

    void Awake()
    {
        life = 1;
        dead = false;
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.collider.CompareTag("Wall"))
            Death();
    }


    public void Death()
    {
        life = 0;
        dead = true;
        animator.SetTrigger("Die");
       
        var move = GetComponent<PlayerMovement>();
        if (move != null) move.enabled = false;
        var shoot = GetComponent<PlayerShoot>();
        if (shoot != null) shoot.enabled = false;
        
        Destroy(gameObject, 1f); 
    }
    

}
