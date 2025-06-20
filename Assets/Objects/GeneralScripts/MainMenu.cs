using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game1"); // Nome exato da cena principal
    }
    

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Fechando o jogo..."); // Só funciona no build, não no editor
    }
}