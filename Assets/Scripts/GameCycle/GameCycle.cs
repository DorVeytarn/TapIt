using System;
using System.Collections.Generic;
using UnityEngine;

public class GameCycle : MonoBehaviour
{
    [Header("Debug and Test")]
    [SerializeField] private bool immortalMode;
    [Space(5)]

    [SerializeField] private float bpm;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private SoundBpmEventLauncher eventLauncher;
    [SerializeField] private StateTimerHandler stateTimerHandler;
    [SerializeField] private ClickHandler clickHandler;

    public event Action GameStarted;
    public event Action GameWon;
    public event Action GameIsLost;

    private void Start()
    {
        stateTimerHandler.TimeOut += OnTimeOut;
        clickHandler.FalseClicked += OnFalseClicked;
    }

    private void OnDestroy()
    {
        stateTimerHandler.TimeOut -= OnTimeOut;
        clickHandler.FalseClicked -= OnFalseClicked;
    }

    public void StartGame()
    {
        eventLauncher.StartBitCalling(bpm, curve);
        GameStarted?.Invoke();
    }

    public void LoseGame()
    {
        eventLauncher.StopBitCalling();
        GameIsLost?.Invoke();
    }

    private void OnTimeOut()
    {
        if (immortalMode)
            return;

        LoseGame();
        Debug.LogError("OnTimeOut");
    }

    private void OnFalseClicked()
    {
        if (immortalMode)
            return;

        LoseGame();
        Debug.LogError("OnFalseClicked");
    }
}
