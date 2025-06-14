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
    private int wave = 0;

    public Image[] lifeSprites;
    private Color32 active = new Color32(1, 1, 1, 1);   
    private Color32 inactive = new Color32(1, 1, 1, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
    }

    public static void UpdateLives(int l)
    {
        foreach (Image i in Instance.lifeSprites)
            i.color = Instance.inactive;
        for (int i = 0; i < l; i++)
        {
            Instance.lifeSprites[i].color = Instance.active;
        }
    }

    public static void UpdateScore(int s)
    {
        Instance.score += s;
        Instance.scoreText.text = Instance.score.ToString("000000");
    }

    public static void UpdateHighscore(int s)
    {
        
    }

    public static void UpdateWave(int w)
    {
        Instance.wave++;
        Instance.waveText.text = Instance.wave.ToString();
    }
}
