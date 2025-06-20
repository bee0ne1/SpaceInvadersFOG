using System;
using System.Collections;
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
        if (other.CompareTag("EnemyBullet") || other.CompareTag("Enemy")) 
        {
            Death();
        }
    }

    public void Death()
    {
        life = 0;
        dead = true;
        
        GameManager.Instance?.PauseEnemies(); 

        var rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;

        var move = GetComponent<PlayerMovement>();
        if (move != null) move.enabled = false;

        var shoot = GetComponent<PlayerShoot>();
        if (shoot != null) shoot.enabled = false;

        animator.SetTrigger("Die");
        
        
        StartCoroutine(DeathAnimationTime());
        
    }

    public IEnumerator DeathAnimationTime()
    {
        yield return new WaitForSeconds(1f);
        
        GameManager.PlayerDeathController();
        
    }
    

}
