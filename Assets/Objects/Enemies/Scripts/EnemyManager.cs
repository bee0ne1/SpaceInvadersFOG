using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [Header("Prefabs dos inimigos (1 por linha)")]
    public GameObject[] enemyPrefabs; // 1 prefab por linha, cada um com Animator

    private int rows;
    private int cols = 10;
    private float spacing = 1.5f;
    private Enemy[,] grid;

    private float moveSpeed = 0.3f;
    private float moveInterval;
    private float moveTimer = 0f;
    private int moveDirection = 1;
    private bool shouldChangeDirection = false;
    private bool toggleAnimation = false;

    void Start()
    {
        rows = enemyPrefabs.Length;
        grid = new Enemy[rows, cols];
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
            }
        }
    }

    void Update()
    {
        moveTimer += Time.deltaTime;

        int remainingEnemies = CountRemainingEnemies();
        float difficultyFactor = Mathf.Clamp01(1f - (remainingEnemies / (float)(rows * cols)));
        moveInterval = Mathf.Lerp(0.05f, 0.5f, 1f - difficultyFactor);

        if (moveTimer >= moveInterval)
        {
            MoveGrid();
            moveTimer = 0f;
        }
    }

    void MoveGrid()
    {
        if (shouldChangeDirection)
        {
            moveDirection *= -1;
            transform.position += Vector3.down * spacing;
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
                        animator.Play(toggleAnimation ? "default1" : "default2");
                    }
                }
            }
        }
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