using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager Instance;
    
    public TextMeshProUGUI scoreText;
    private int score;
    
    public TextMeshProUGUI highscoreText;
    private int highscore;
    
    public TextMeshProUGUI waveText;
    private int wave;

    public Image[] lifeSprites;
    private Color32 active = new Color32(255, 255, 255, 255);   
    private Color32 inactive = new Color32(255, 255, 255, 10);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
    }

    private void Start()
    {
        Instance.wave = 1;
        Instance.waveText.text = Instance.wave.ToString();
    }

    public static void UpdateLives(int l)
    {
        if (Instance == null || Instance.lifeSprites == null)
        {
            return;
        }
        
        l = Mathf.Clamp(l, 0, Instance.lifeSprites.Length);

        for (int i = 0; i < Instance.lifeSprites.Length; i++)
        {
            // Ativa os ícones até o número de vidas
            Instance.lifeSprites[i].color = (i < l) ? Instance.active : Instance.inactive;
        }
        
    }

    public static void UpdateScore(int s)
    {
        Instance.score += s;
        Instance.scoreText.text = Instance.score.ToString("000000");
    }

    public static void ResetScore()
    {
        Instance.score = 0;
        Instance.scoreText.text = Instance.score.ToString("000000");
    }
    public static void UpdateHighscore(int s)
    {
        
    }

    public static void UpdateWave()
    {
        Instance.wave++;
        Instance.waveText.text = Instance.wave.ToString();
    }

    public static void ResetWave()
    {
        Instance.Start();
    }
}
