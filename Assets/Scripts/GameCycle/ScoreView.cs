using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreHandler scoreHandler;
    [SerializeField] private Slider scoreBar;
    [SerializeField] private Text scoreCounter;
    [SerializeField] private float[] scoreGradations;

    private int gradationIndex;
    private Vector2 scoreRange;

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

        scoreCounter.text = ((int)value).ToString();
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
}
