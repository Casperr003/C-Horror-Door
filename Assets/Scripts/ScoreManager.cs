using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score = 0;
    [Header("Win Settings")]
    public int targetScore = 20;
    public string completeSceneName = "GameComplete";
    private bool gameFinished = false;

    void Start()
    {
        UpdateUI();
    }
    public void AddPoint()
    {
        if (gameFinished)
            return;
        score++;
        Debug.Log("Score: " + score);
        UpdateUI();
        if (score >= targetScore)
        {
            gameFinished = true;

            Debug.Log("Target reached. Loading scene: " + completeSceneName);

            SceneManager.LoadScene(completeSceneName);
        }
    }
    public void ResetScore()
    {
        if (gameFinished)
            return;
        score = 0;
        UpdateUI();
    }
    void UpdateUI()
    {
        scoreText.text =
            "Days since last accident: " + score.ToString();
    }
}