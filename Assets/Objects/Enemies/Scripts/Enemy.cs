using System;
using UnityEngine;
using System.Collections;

public class Enemy : Shooter, ICharacterStats
{
    public int life { get; set; }
    public bool dead { get; set; }
    
    private EnemyManager manager;
    private Animator animator;
<<<<<<< Updated upstream
    private Bullet currentBullet;
=======
    //private Bullet currentBullet;
>>>>>>> Stashed changes
    private int row;
    private int col;
    
    void Awake()
    {
        life = 1;
        dead = false;
        direction = Vector2.down;
        animator = GetComponent<Animator>();
    }

<<<<<<< Updated upstream
=======
    private void Update()
    {
        CanShoot();
    }

>>>>>>> Stashed changes
    // Método que o EnemyManager chama para registrar a posição do inimigo
    public void SetPositionInGrid(int r, int c, EnemyManager m)
    {
        row = r;
        col = c;
        manager = m;
    }
    
    public override void CanShoot()
    {
<<<<<<< Updated upstream
        if (manager == null || dead) return;
            
        Shoot();
        
    }
    
    public override void Shoot()
    {
        SpawnBullet();
    }
    
=======
        if (!manager.HasEnemyBelow(row, col) && !dead)
        {
            Shoot();
        }
    }
    
    public override void Shoot()
    {
        //SpawnBullet();
    }
>>>>>>> Stashed changes
    public void Death()
    {
        life = 0;
        dead = true;
        animator.Play("dying");
        Destroy(gameObject,0.2f);
    }
<<<<<<< Updated upstream
    
    
=======

>>>>>>> Stashed changes
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
            manager.NotifyWallHit();
        if (other.CompareTag("PlayerBullet"))
            Death();
    }
    
    
}