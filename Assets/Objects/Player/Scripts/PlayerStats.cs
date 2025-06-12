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
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet")) // ou o nome da tag da sua bala
        {
            Death();
        }
    }


    public void Death()
    {
        life = 0;
        dead = true;
        animator.SetTrigger("Die");
       
        var rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        var move = GetComponent<PlayerMovement>();
        if (move != null) move.enabled = false;
        var shoot = GetComponent<PlayerShoot>();
        if (shoot != null) shoot.enabled = false;
        
        Destroy(gameObject, 1f); 
    }
    

}
