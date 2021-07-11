using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private const float frameRate = 60;

    [SerializeField] private GameCycle gameCycle;
    [SerializeField] private ClickHandler clickHandler;
    [SerializeField] private float scoreStep;
    [SerializeField] private float secondsStep;
    [SerializeField] private float oneTimeAccrualingScoreValue;
    [SerializeField] private float oneTimeAccrualingSeconds;

    private float currentScore;
    private Coroutine permanentScoreAccrualing;
    private Coroutine oneTimeScoreAccrualing;

    public float CurrentScore => currentScore;
    public event Action<float> ScoreChanged;

    private void OnEnable()
    {
        gameCycle.GameStarted += OnGameStarted;
        gameCycle.GameIsLost += OnGameIsLost;
        clickHandler.TrueClicked += OnTrueClicked;
    }

    private void OnDisable()
    {
        gameCycle.GameStarted -= OnGameStarted;
        gameCycle.GameIsLost -= OnGameIsLost;
        clickHandler.TrueClicked -= OnTrueClicked;
    }

    private void OnGameStarted()
    {
        if (permanentScoreAccrualing != null)
        {
            StopCoroutine(permanentScoreAccrualing);
            permanentScoreAccrualing = null;
        }

        permanentScoreAccrualing = StartCoroutine(PremanentScoreAccrualing());
    }

    private void OnGameIsLost()
    {
        if (permanentScoreAccrualing != null)
        {
            StopCoroutine(permanentScoreAccrualing);
            permanentScoreAccrualing = null;
        }
    }

    private void OnTrueClicked()
    {
        if (oneTimeScoreAccrualing != null)
            return;

        oneTimeScoreAccrualing = StartCoroutine(OneTimeScoreAccrualing(oneTimeAccrualingScoreValue,
                                                                       oneTimeAccrualingSeconds,
                                                                       true));
    }

    private IEnumerator PremanentScoreAccrualing()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsStep);

            //Debug.Log($"ScoreHandler + PremanentScoreAccrualing + {currentScore}");
            currentScore += scoreStep;
            ScoreChanged?.Invoke(currentScore);
        }
    }

    private IEnumerator OneTimeScoreAccrualing(float value, float seconds, bool continueAccrual)
    {
        if (permanentScoreAccrualing != null)
        {
            StopCoroutine(permanentScoreAccrualing);
            permanentScoreAccrualing = null;
        }

        float newScore = currentScore + value;
        float step = value / (seconds * frameRate);

        while (currentScore <= newScore)
        {
            currentScore += step;
            ScoreChanged?.Invoke(currentScore);

            //Debug.Log($"ScoreHandler + OneTimeScoreAccrualing + {currentScore}");
            yield return null;
        }

        if (continueAccrual)
        {
            permanentScoreAccrualing = StartCoroutine(PremanentScoreAccrualing());
        }

        oneTimeScoreAccrualing = null;
    }
}
