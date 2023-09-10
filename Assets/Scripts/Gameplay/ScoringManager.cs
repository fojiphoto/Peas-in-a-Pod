using UnityEngine;
using UnityEngine.UI;

public class ScoringManager : MonobehaviourSingleton<ScoringManager>
{
    private int currentScore;
    private int previousHighScore;

    [Range(100, 500)]
    [SerializeField] private int ScoreRange;

    private int targetScore;
    private float animationDuration = 2f;
    private void Start()
    {
        previousHighScore = PlayerPrefsManager.Get(PlayerPrefsManager.HighScore, 0);

        // Start the counting animation
    }
    public int GetScore() 
    {
        int score =  PlayerPrefsManager.Get(PlayerPrefsManager.CurrentLevel, 0) / 2 * ScoreRange;
        ScoreUpdate(score);
        return score;
    }
    public void ScoreUpdate(int amount)
    {
        currentScore += amount;
        if (currentScore > previousHighScore)
        {
            previousHighScore = currentScore;
            PlayerPrefsManager.Set(PlayerPrefsManager.HighScore, previousHighScore);
        }
    }
}


