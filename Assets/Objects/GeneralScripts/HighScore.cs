using UnityEngine;
using TMPro;

public class MainMenuHighscore : MonoBehaviour
{
    public TextMeshProUGUI highscoreText;

    void Start()
    {
        int highscore = PlayerPrefs.GetInt("HighScore",0);
        highscoreText.text = highscore.ToString("000000");
    }
}