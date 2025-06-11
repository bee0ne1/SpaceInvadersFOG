using UnityEngine;

public class Enemy : Shooter, ICharacterStats
{
    public int life { get; set; }
    public bool dead { get; set; }
    
    private EnemyManager manager;
    private int row;
    private int col;
    
    void Awake()
    {
        life = 1;
        dead = false;
        direction = Vector2.down;
        //animator = GetComponent<Animator>();
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

    public void OnDeath()
    {
        manager.RemoveEnemy(row, col);
        Destroy(gameObject);
    }
    
    protected override void Shoot()
    {
        
    }
}
