using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TargetShower : MonoBehaviour
{
    private enum ButtonState
    {
        Color,
        Image
    }

    private const int timerSeconds = 3;

    [SerializeField] private GameObject[] dependentObjects;
    [SerializeField] private float delay;
    [SerializeField] private ButtonState currentState;
    [SerializeField] private ButtonStateSetter buttonStateSetter;
    [SerializeField] private ColorPalette palette;
    [SerializeField] private Image targetImage;

    [Header("GameCycle")]
    [SerializeField] private GameCycle gameCycle;

    [Header("Timer")]
    [SerializeField] private Image timerImageBar;
    [SerializeField] private Text timerCounter;

    [Header("Main Button")]
    [SerializeField] private UIMove mainButtonMove;

    private Coroutine delayedShoweCallback;

    private void OnEnable()
    {
        gameCycle.GameIsLost += OnGameLost;
    }

    private void OnDisable()
    {
        gameCycle.GameIsLost -= OnGameLost;
    }

    public void ShowTarget(Action callback)
    {
        SetDependentObjectsActive(true);
        mainButtonMove.Move();

        if (currentState == ButtonState.Color)
            ShowColorTarget();

        if(delayedShoweCallback != null)
        {
            StopCoroutine(delayedShoweCallback);
            delayedShoweCallback = null;
        }

        delayedShoweCallback = StartCoroutine(DelayedShowerCallback(callback));
    }

    public void HideTarget()
    {
        SetDependentObjectsActive(false);
        mainButtonMove.Move(false);
    }

    private void ShowColorTarget()
    {
        Color falseColor = palette.Colors[UnityEngine.Random.Range(0, palette.Colors.Count)];

        targetImage.color = falseColor;
        buttonStateSetter.SetColorState(palette.Colors, falseColor);
    }

    private void OnGameLost()
    {
        HideTarget();
    }

    private IEnumerator DelayedShowerCallback(Action callback)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(Timer(timerSeconds, callback));
    }

    private IEnumerator Timer(int seconds, Action callback)
    {
        timerCounter.gameObject.SetActive(true);

        timerCounter.text = seconds.ToString();

        while (seconds > 0)
        {
            var imageTween = DOTweenModuleUI.DOFillAmount(timerImageBar, 0, 1f).OnComplete(() => timerImageBar.fillAmount = 1);

            yield return new WaitForSeconds(1f);
            seconds--;
            timerCounter.text = seconds.ToString();
        }

        timerCounter.text = "";
        timerCounter.gameObject.SetActive(false);

        callback?.Invoke();
    }

    private void SetDependentObjectsActive(bool active)
    {
        for (int i = 0; i < dependentObjects.Length; i++)
        {
            dependentObjects[i].SetActive(active);
        }
    }
}
