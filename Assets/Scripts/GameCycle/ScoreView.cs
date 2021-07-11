using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreHandler scoreHandler;
    [SerializeField] private Text counter;
    [SerializeField] private bool automaticScoreUpdtate;

    private void OnEnable()
    {
        scoreHandler.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        scoreHandler.ScoreChanged -= OnScoreChanged;
        UpdateScore(0);
    }

    public void ShowCustomScore(bool animatedShow = true, float duration = 0.5f,  float from = 0, float to = -1)
    {
        if(to == -1)
            to = scoreHandler.CurrentScore;

        if (animatedShow)
            DOVirtual.Float(from, to, duration, UpdateScore);
        else
            UpdateScore(to);
    }

    private void OnScoreChanged(float value)
    {
        if (automaticScoreUpdtate)
            UpdateScore(value);
    }

    private void UpdateScore(float value)
    {
        counter.text = ((int)value).ToString();
    }
}
