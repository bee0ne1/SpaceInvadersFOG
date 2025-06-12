using UnityEngine;
using System.Collections;

public class Enemy : Shooter, ICharacterStats
{
    public int life { get; set; }
    public bool dead { get; set; }
    
    private EnemyManager manager;
    public Animator animator;
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
    
    public bool CanShoot()
    {
        return !manager.HasEnemyBelow(row, col);
    }

    public void Death()
    {
        life = 0;
        dead = true;
        animator.Play("dying");
        Destroy(gameObject,0.2f);
    }
    
    public override void Shoot()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
            manager.NotifyWallHit();
        if (other.CompareTag("PlayerBullet"))
            Death();
    }
    
    
}
