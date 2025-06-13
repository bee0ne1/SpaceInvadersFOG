using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

    private void Start()
    {
        SpawnBunkers();
        GameObject waveGO = Instantiate(enemyWavePrefab, enemiesSpawnPoint.position, enemiesSpawnPoint.rotation);
        EnemyManager enemyManager = waveGO.GetComponent<EnemyManager>();
        StartCoroutine(WaitForEnemiesToSpawn(enemyManager));
    }

    void SpawnBunkers()
    {
        Instantiate(bunkerPrefab, bunker1position.position, bunker1position.rotation);
        Instantiate(bunkerPrefab, bunker2position.position, bunker2position.rotation);
        Instantiate(bunkerPrefab, bunker3position.position, bunker3position.rotation);
        Instantiate(bunkerPrefab, bunker4position.position, bunker4position.rotation);
    }
    private IEnumerator WaitForEnemiesToSpawn(EnemyManager enemyManager)
    {
        yield return new WaitUntil(() => enemyManager.allEnemiesSpawned);

        Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        
        RespawnUFOWithDelay();
    }
    
    public void RespawnUFOWithDelay()
    {
        StartCoroutine(UFORespawnRoutine());
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
        RespawnUFOWithDelay();
    }
    public void ClearUFO()
    {
        currentUFO = null;
    }
}