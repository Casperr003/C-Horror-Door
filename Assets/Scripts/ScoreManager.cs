using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    int score = 0;
    void Awake()
    {
        if (scoreText == null)
        {
            scoreText = FindFirstObjectByType<TMP_Text>();
        }

        if (scoreText == null)
        {
            Debug.LogError("No TMP_Text found for ScoreManager");
        }
    }
    void Start()
    {
        UpdateUI();
    }
    public void AddPoint()
    {
        score++;
        UpdateUI();
    }
    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }
    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text =
                "Days since last accident: " + score;
        }
    }
}