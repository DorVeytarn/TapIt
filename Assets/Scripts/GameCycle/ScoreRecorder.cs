using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    [SerializeField] private ScoreHandler scoreHandler;


    private void OnEnable()
    {
        scoreHandler.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        scoreHandler.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(float value)
    {
        var highScore = PlayerPrefsUtility.GetInt(GlobalConst.HighScoreKey);

        if(highScore != null && highScore.Value < value)
        {
            PlayerPrefsUtility.SaveInt(GlobalConst.HighScoreKey, (int)value);
        }
    }
}
