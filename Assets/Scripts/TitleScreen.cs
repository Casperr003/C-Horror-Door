using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [Header("Scene Names")]
    public string gameScene = "CHorrorDoor";
    public void PlayGame()
    {
        SceneManager.LoadScene(gameScene);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}