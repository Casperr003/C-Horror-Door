using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonManager : MonoBehaviour
{
    [Header("Scene Name")]
    public string titleScene = "TitleScreen";
    void Start()
    {
        UnlockCursor();
    }
    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ReturnToTitle()
    {
        SceneManager.LoadScene(titleScene);
    }
}