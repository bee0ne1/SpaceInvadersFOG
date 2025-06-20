using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int lives;
    private bool afterWin = false;
    private GameObject currentWaveGO;
    private GameObject currentPlayer;
    private GameObject[] currentBunkers = new GameObject[4];
    
    
    [Header("Player")] 
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    [Header("UFO")]
    public GameObject ufoPrefab;
    public float minUfoRespawnDelay = 10f;
    public float maxUfoRespawnDelay = 30f;
    private GameObject currentUFO;

    [Header("EnemyWave")] 
    public GameObject enemyWavePrefab;
    public Transform enemiesSpawnPoint;
    private EnemyManager enemyManager;
    
    [Header("Bunker")] 
    public GameObject bunkerPrefab;
    public Transform bunker1position;
    public Transform bunker2position;
    public Transform bunker3position;
    public Transform bunker4position;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (enemyManager != null && enemyManager.CountRemainingEnemies() == 0 && !afterWin)
        {
            afterWin = true;
            StartCoroutine(GameWin());
        }
    }

    private void Start()
    {
        if (currentUFO != null)
        {
            Destroy(currentUFO);
            currentUFO = null;
        }

        // Destruir player anterior
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
            currentPlayer = null;
        }

        // Destruir wave de inimigos anterior
        if (currentWaveGO != null)
        {
            Destroy(currentWaveGO);
            currentWaveGO = null;
        }
        
        for (int i = 0; i < currentBunkers.Length; i++)
        {
            if (currentBunkers[i] != null)
            {
                Destroy(currentBunkers[i]);
                currentBunkers[i] = null;
            }
        }

        if (!afterWin)
        {
            lives = 3;
            UIManager.UpdateLives(lives);
        }

        SpawnBunkers();
        currentWaveGO = Instantiate(enemyWavePrefab, enemiesSpawnPoint.position, enemiesSpawnPoint.rotation);
        enemyManager = currentWaveGO.GetComponent<EnemyManager>();
        StartCoroutine(WaitForEnemiesToSpawn(enemyManager));
    
    }

    private void StartAgain()
    {
        UnpauseEnemies();
        
        if (currentUFO != null)
        {
            Destroy(currentUFO);
            currentUFO = null;
        }

        // Destruir player anterior
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
            currentPlayer = null;
        }
        
        currentPlayer = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation); 
    }
    
    
    void SpawnBunkers()
    {
        currentBunkers[0] = Instantiate(bunkerPrefab, bunker1position.position, bunker1position.rotation);
        currentBunkers[1] = Instantiate(bunkerPrefab, bunker2position.position, bunker2position.rotation);
        currentBunkers[2] = Instantiate(bunkerPrefab, bunker3position.position, bunker3position.rotation);
        currentBunkers[3] = Instantiate(bunkerPrefab, bunker4position.position, bunker4position.rotation);

    }
    private IEnumerator WaitForEnemiesToSpawn(EnemyManager enemyManager)
    {
        yield return new WaitUntil(() => enemyManager.allEnemiesSpawned);
        
        afterWin = false;
        
        currentPlayer = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        
        StartCoroutine(UFORespawnRoutine());
    }

    public static void PlayerDeathController()
    {
        if (Instance.lives > 0)
        {
            Instance.lives--;
            UIManager.UpdateLives(Instance.lives);
            Instance.StartAgain();
        }
        else
        {
            GameOver();
        }
    }

    public static void GameOver()
    {
        //COLOCAR FUNCAO QUE SALVA O SCORE E RESETA
        UIManager.ResetScore();
        UIManager.ResetWave();
        Instance.Start();
    }

    private IEnumerator GameWin()
    {
        yield return new WaitForSeconds(1.5f);
        UIManager.UpdateWave();
        Instance.Start();
    }
    
    
    private IEnumerator UFORespawnRoutine()
    {
        float randomDelay = UnityEngine.Random.Range(minUfoRespawnDelay, maxUfoRespawnDelay);
        yield return new WaitForSeconds(randomDelay);

        if (ufoPrefab != null && currentUFO == null)
        {
            currentUFO = Instantiate(ufoPrefab);
            currentUFO.SetActive(true);
        }

        // Agendar o próximo respawn
        StartCoroutine(UFORespawnRoutine());
    }
    
    public void ClearUFO()
    {
        currentUFO = null;
    }
    
    public void PauseEnemies()
    {
        if (enemyManager != null)
        {
            enemyManager.isPaused = true;
        }
    }

    public void UnpauseEnemies()
    {
        if (enemyManager != null)
        {
            enemyManager.isPaused = false;
        }
    }
    
    
    
}