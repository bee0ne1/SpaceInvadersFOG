using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject); // Destrói o GameManager antigo
            GameManager.Instance = null;
        }

        SceneManager.LoadScene("Game1");
    }
    

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Fechando o jogo..."); // Só funciona no build, não no editor
    }
}