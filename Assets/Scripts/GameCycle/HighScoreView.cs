using UnityEngine;

public class HighScoreView : ScoreView
{
    [Header("High Score")]
    [SerializeField] private ScoreRecorder scoreRecorder;
    [SerializeField] private bool automaticHighScoreUpdate;

    private void Awake()
    {
        if (automaticHighScoreUpdate)
            automaticScoreUpdtate = false;
    }

    protected override void OnEnable()
    {
        scoreRecorder.HighScoreChanged += OnHighScoreChanged;
        UpdateScore(scoreRecorder.HighScore);
    }

    protected override void OnDisable()
    {
        scoreRecorder.HighScoreChanged -= OnHighScoreChanged;
    }

    private void OnHighScoreChanged(float value)
    {
        if (automaticHighScoreUpdate)
            UpdateScore(value);
    }
}
