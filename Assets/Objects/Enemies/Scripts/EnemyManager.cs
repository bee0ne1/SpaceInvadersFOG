using UnityEngine;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private int rows = 5;
    private int cols = 10;
    private float spacing = 1.5f;
    private Enemy[,] grid;
    public Sprite[] enemySprites; 
    
    void Start()
    {
        grid = new Enemy[rows, cols];
        Vector2 offset = new Vector2((cols - 1) * spacing / 2f, -(rows - 1) * spacing / 2f);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector2 localPos = new Vector2(col * spacing, row * -spacing) - offset;

                // Instancia o inimigo como filho do EnemyManager
                GameObject enemyGO = Instantiate(enemyPrefab, transform);
                enemyGO.transform.localPosition = localPos;

                // Troca o sprite baseado na linha (se houver sprite suficiente)
                SpriteRenderer sr = enemyGO.GetComponent<SpriteRenderer>();
                if (sr != null && row < enemySprites.Length)
                {
                    sr.sprite = enemySprites[row];
                }

                // Armazena o inimigo na grade
                Enemy enemy = enemyGO.GetComponent<Enemy>();
                grid[row, col] = enemy;
                enemy.SetPositionInGrid(row, col, this);
            }
        }
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
        
        
}
