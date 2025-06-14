using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    [Header("Prefabs dos inimigos (1 por linha)")]
    public GameObject[] enemyPrefabs; // 1 prefab por linha, cada um com Animator

    private int rows;
    private int cols = 11;
    private float spacing = 0.6f;
    private Enemy[,] grid;

    private float moveSpeed = 0.1f;
    private float moveInterval;
    private float moveTimer = 0f;
    private float shootTimer = 0f;
    private float shootInterval;
    [SerializeField] public bool allEnemiesSpawned = false;
    private int moveDirection = 1;
    private bool shouldChangeDirection = false;
    private bool toggleAnimation = false;

    void Start()
    {
        rows = enemyPrefabs.Length;
        grid = new Enemy[rows, cols];

        StartCoroutine(SpawnEnemiesGradually());
    }
    
    private IEnumerator SpawnEnemiesGradually()
    {
        Vector2 offset = new Vector2((cols - 1) * spacing / 2f, -(rows - 1) * spacing / 2f);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector2 localPos = new Vector2(col * spacing, row * -spacing) - offset;

                GameObject enemyGO = Instantiate(enemyPrefabs[row], transform);
                enemyGO.transform.localPosition = localPos;

                Enemy enemy = enemyGO.GetComponent<Enemy>();
                grid[row, col] = enemy;
                enemy.SetPositionInGrid(row, col, this);

                yield return new WaitForSeconds(0.03f); // Pequeno intervalo entre os inimigos
            }
        }
        allEnemiesSpawned = true;
    }

    void Update()
    {
        if (allEnemiesSpawned)
        {
            moveTimer += Time.deltaTime;

            int remainingEnemies = CountRemainingEnemies();
            float difficultyFactor = Mathf.Clamp01(1f - (remainingEnemies / (float)(rows * cols)));
            
            if (remainingEnemies <= 1)
                moveInterval = 0.02f;
            else
                moveInterval = Mathf.Lerp(1.0f, 0.02f, difficultyFactor);
            
            float rawShootInterval = Mathf.Lerp(1.5f, 0.3f, difficultyFactor);
            shootInterval = Mathf.Max(rawShootInterval, 1.0f);


            if (moveTimer >= moveInterval)
            {
                MoveGrid();
                moveTimer = 0f;
            }

            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                TryShoot();
                shootTimer = 0f;
            }
        }
    }
    
    
    
    void MoveGrid()
    {
        if (shouldChangeDirection)
        {
            moveDirection *= -1;
            transform.position += Vector3.down * 2f; //spacing/2;
            shouldChangeDirection = false;
        }
        else
        {
            transform.position += Vector3.right * moveDirection * moveSpeed;
        }

        toggleAnimation = !toggleAnimation;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Enemy enemy = grid[row, col];
                if (enemy != null && !enemy.dead)
                {
                    Animator animator = enemy.GetComponent<Animator>();
                    if (animator != null)
                    {
                        animator.Play(toggleAnimation ? "default2" : "default1");
                    }
                }
            }
        }
    }
    
    
    void TryShoot()
    {
        // Encontra todos os inimigos vivos na cena
        if (CountRemainingEnemies() == 0) return;

        // Agrupa por coluna (X arredondado) e pega o de menor Y (mais embaixo)
        var bottomEnemies = GetBottomEnemies();
        
        // Escolhe um aleatório
        Enemy chosen = bottomEnemies[Random.Range(0, bottomEnemies.Count)];

        // Faz ele atirar
        Shooter shooter = chosen.GetComponent<Shooter>();
        shooter.CanShoot();
        
    }

    public void NotifyWallHit()
    {
        shouldChangeDirection = true;
    }

    public bool HasEnemyBelow(int row, int col)
    {
        for (int r = row + 1; r < rows; r++)
        {
            if (grid[r, col] != null)
                return true;
        }
        return false;
    }

    public void RemoveEnemy(int row, int col)
    {
        grid[row, col] = null;
    }

    public List<Enemy> GetBottomEnemies()
    {
        List<Enemy> bottomEnemies = new List<Enemy>();
        for (int col = 0; col < cols; col++)
        {
            for (int row = rows - 1; row >= 0; row--)
            {
                if (grid[row, col] != null)
                {
                    bottomEnemies.Add(grid[row, col]);
                    break;
                }
            }
        }
        return bottomEnemies;
    }

    private int CountRemainingEnemies()
    {
        int count = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (grid[row, col] != null)
                    count++;
            }
        }
        return count;
    }
}