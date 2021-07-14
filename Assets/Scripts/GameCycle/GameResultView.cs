using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResultView : MonoBehaviour
{
    [SerializeField] private ScoreHandler scoreHandler;
    [SerializeField] private ScoreRecorder scoreRecorder;
    [SerializeField] private GameCycle gameCycle;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private Button continueButton;

    [Header("Score Counter")]
    [SerializeField] private ScoreView scoreView;
    [SerializeField] private float showingDuration;
    [SerializeField] private GameObject highScoreObject;

    [Header("Result Phrase")]
    [SerializeField] private Text resultPhrase;

    [Header("UIMoves")]
    [SerializeField] private UIMoveSequence showSequence;
    [SerializeField] private UIMoveSequence hideSequence;

    private bool isNewHighScore;

    private void OnEnable()
    {
        continueButton.onClick.AddListener(OnContinueButtonClick);
        scoreRecorder.HighScoreChanged += OnHighScoreChanged;
        gameCycle.GameIsLost += OnGameIsLost;
    }

    private void OnDisable()
    {
        continueButton.onClick.RemoveListener(OnContinueButtonClick);
        scoreRecorder.HighScoreChanged -= OnHighScoreChanged;
        gameCycle.GameIsLost -= OnGameIsLost;
    }
    private void OnHighScoreChanged(float value)
    {
        isNewHighScore = true;
    }

    private void OnContinueButtonClick()
    {
        highScoreObject.SetActive(false);

        if (hideSequence != null)
            hideSequence.Move(OnHidden);
        else
            OnHidden();
    }

    private void OnGameIsLost()
    {
        resultPanel.SetActive(true);

        if (showSequence != null)
            showSequence.Move(OnShowed);
        else
            OnShowed();
    }

    private void OnShowed()
    {
        continueButton.gameObject.SetActive(true);
        scoreView.ShowCustomScore(true, showingDuration);

        if (isNewHighScore)
        {
            isNewHighScore = false;
            highScoreObject.SetActive(true);
        }
    }

    private void OnHidden()
    {
        resultPanel.SetActive(false);
        continueButton.gameObject.SetActive(false);
        scoreView.ShowCustomScore(false, 0, 0, 0);
    }
}
