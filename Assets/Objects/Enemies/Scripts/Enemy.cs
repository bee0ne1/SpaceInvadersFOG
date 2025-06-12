using UnityEngine;
using System.Collections;

public class Enemy : Shooter, ICharacterStats
{
    public int life { get; set; }
    public bool dead { get; set; }
    
    private EnemyManager manager;
<<<<<<< HEAD
    private Animator animator;
<<<<<<< Updated upstream
    private Bullet currentBullet;
=======
    //private Bullet currentBullet;
>>>>>>> Stashed changes
=======
    public Animator animator;
>>>>>>> parent of 6d5a5f5 (enemy shooting system)
    private int row;
    private int col;
    
    void Awake()
    {
        life = 1;
        dead = false;
        direction = Vector2.down;
        animator = GetComponent<Animator>();
    }
<<<<<<< HEAD

<<<<<<< Updated upstream
=======
    private void Update()
    {
        CanShoot();
    }

>>>>>>> Stashed changes
=======
    
    
>>>>>>> parent of 6d5a5f5 (enemy shooting system)
    // Método que o EnemyManager chama para registrar a posição do inimigo
    public void SetPositionInGrid(int r, int c, EnemyManager m)
    {
        row = r;
        col = c;
        manager = m;
    }
    
    public bool CanShoot()
    {
<<<<<<< HEAD
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
=======
        return !manager.HasEnemyBelow(row, col);
    }

>>>>>>> parent of 6d5a5f5 (enemy shooting system)
    public void Death()
    {
        life = 0;
        dead = true;
        animator.Play("dying");
        Destroy(gameObject,0.2f);
    }
<<<<<<< Updated upstream
    
<<<<<<< HEAD
    
=======

>>>>>>> Stashed changes
=======
    public override void Shoot()
    {
        
    }

>>>>>>> parent of 6d5a5f5 (enemy shooting system)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
            manager.NotifyWallHit();
        if (other.CompareTag("PlayerBullet"))
            Death();
    }
    
    
}
