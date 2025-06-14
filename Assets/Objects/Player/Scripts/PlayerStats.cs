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
        if (other.CompareTag("EnemyBullet") || other.CompareTag("Enemy")) // ou o nome da tag da sua bala
        {
            Death();
        }
    }

    public void Death()
    {
        life = 0;
        dead = true;

        var rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;

        var move = GetComponent<PlayerMovement>();
        if (move != null) move.enabled = false;

        var shoot = GetComponent<PlayerShoot>();
        if (shoot != null) shoot.enabled = false;

        animator.SetTrigger("Die");
    }

    /*
    if (GameManager.totalLives > 0)
    {
        GameManager.totalLives--;
        UIManager.UpdateLives(GameManager.totalLives);
        Invoke(nameof(RespawnAfterDelay), 1f); // Aguarda animação de morte
    }
    else
    {
        Invoke(nameof(GameOver), 1f);
    }
}
private void RespawnAfterDelay()
{
    GameManager.Instance.RespawnAfterDeath();
    Destroy(gameObject);
}

private void GameOver()
{
    GameManager.Instance.GameOver();
    Destroy(gameObject);
}
*/
    

}
