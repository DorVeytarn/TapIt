using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StateTimerHandler : MonoBehaviour
{
    [SerializeField] private SoundBpmEventLauncher eventLauncher;
    [SerializeField] private ButtonStateSetter buttonState;
    [SerializeField] private ClickHandler clickHandler;
    [SerializeField] private Image image;
    [SerializeField] private float fillImageTime = 0.5f;

    private bool isTrueClicked;

    public event Action TimeOut;

    private void Start()
    {
        eventLauncher.BitPlayed += OnBitPlayed;
        clickHandler.TrueClicked += OnTrueClicked;
        clickHandler.FalseClicked += OnFalseClicked;
    }

    private void OnDestroy()
    {
        eventLauncher.BitPlayed -= OnBitPlayed;
        clickHandler.TrueClicked -= OnTrueClicked;
        clickHandler.FalseClicked -= OnFalseClicked;
    }

    private void OnBitPlayed()
    {
        EmptyImage();
    }

    private void OnFalseClicked()
    {
        FillImage(fillImageTime);
    }

    private void OnTrueClicked()
    {
        isTrueClicked = true;
    }

    private void EmptyImage()
    {
        var tween = DOTweenModuleUI.DOFillAmount(image, 0f, eventLauncher.SecondToOneBit - fillImageTime);
        tween.OnComplete(() =>
        {
            if (buttonState.IsTrueState && isTrueClicked == false)
                TimeOut?.Invoke();
            else
                isTrueClicked = false;

            image.fillAmount = 1;
        });
    }

    private void FillImage(float time)
    {
        DOTweenModuleUI.DOFillAmount(image, 1f, time);
    }
}
