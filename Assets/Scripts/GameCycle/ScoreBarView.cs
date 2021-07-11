using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarView : MonoBehaviour
{
    [SerializeField] private ScoreHandler scoreHandler;
    [SerializeField] private GameCycle gameCycle;
    [SerializeField] private Slider scoreBar;
    [SerializeField] private float[] scoreGradations;

    private int gradationIndex;
    private Vector2 scoreRange;

    private void OnEnable()
    {
        scoreHandler.ScoreChanged += OnScoreChanged;
        gameCycle.GameIsLost += OnGameIsLost;
    }

    private void OnDisable()
    {
        scoreHandler.ScoreChanged -= OnScoreChanged;
        gameCycle.GameIsLost -= OnGameIsLost;
    }

    private void Start()
    {
        SetNextGradationIndex();
        UpdateScoreBar();
    }

    private void OnScoreChanged(float value)
    {
        bool isSuitableRange = value >= scoreRange.x && value <= scoreRange.y;

        if (isSuitableRange == false)
        {
            SetNextGradationIndex();
            UpdateScoreBar();
        }

        scoreBar.value = value;
    }

    private void SetNextGradationIndex()
    {
        float startValue = (gradationIndex >= scoreGradations.Length - 1) ? scoreRange.y : scoreGradations[gradationIndex];
        float finishValue = (gradationIndex >= scoreGradations.Length - 1) ? startValue * startValue : scoreGradations[gradationIndex + 1];

        scoreRange = new Vector2(startValue, finishValue);

        gradationIndex++;
    }

    private void UpdateScoreBar()
    {
        scoreBar.minValue = scoreRange.x;
        scoreBar.maxValue = scoreRange.y;
    }

    private void OnGameIsLost()
    {
        gradationIndex = 0;

        scoreRange = new Vector2(scoreGradations[gradationIndex], scoreGradations[gradationIndex + 1]);
        UpdateScoreBar();
    }
}
