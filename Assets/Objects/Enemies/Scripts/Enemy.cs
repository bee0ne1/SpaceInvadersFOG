using System;
using UnityEngine;
using System.Collections;

public class Enemy : Shooter, ICharacterStats
{
    public int life { get; set; }
    public bool dead { get; set; }
    
    private EnemyManager manager;
    private Animator animator;
    private Bullet currentBullet;
    private int row;
    private int col;
    
    void Awake()
    {
        life = 1;
        dead = false;
        direction = Vector2.down;
        animator = GetComponent<Animator>();
    }

    // Método que o EnemyManager chama para registrar a posição do inimigo
    public void SetPositionInGrid(int r, int c, EnemyManager m)
    {
        row = r;
        col = c;
        manager = m;
    }
    
    public override void CanShoot()
    {
        if (manager == null || dead) return;
            
        Shoot();
        
    }
    
    public override void Shoot()
    {
        SpawnBullet();
    }
    
    public void Death()
    {
        life = 0;
        dead = true;
        animator.Play("dying");
        Destroy(gameObject,0.2f);
    }
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
            manager.NotifyWallHit();
        if (other.CompareTag("PlayerBullet"))
            Death();
    }
    
    
}