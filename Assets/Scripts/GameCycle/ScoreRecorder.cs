using System;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    [SerializeField] private ScoreHandler scoreHandler;

    public int HighScore { get; private set; }

    public event Action<float> HighScoreChanged; 

    private void OnEnable()
    {
        scoreHandler.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        scoreHandler.ScoreChanged -= OnScoreChanged;
    }

    private void Start()
    {
        var highScore = PlayerPrefsUtility.GetInt(GlobalConst.HighScoreKey);

        if (highScore != null)
            HighScore = highScore.Value;
        else
            HighScore = 0;
    }

    private void OnScoreChanged(float value)
    {
        var highScore = PlayerPrefsUtility.GetInt(GlobalConst.HighScoreKey);

        if(highScore == null || highScore.Value < value)
        {
            HighScore = (int)value;
            PlayerPrefsUtility.SaveInt(GlobalConst.HighScoreKey, HighScore);
            HighScoreChanged?.Invoke(HighScore);
        }
    }
}
