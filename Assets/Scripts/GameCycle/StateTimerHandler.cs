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
    [SerializeField] private float additionalDelayBeforeTimeOut = 0.1f;

    private float barStep;
    private float frames;
    private bool isTrueClicked;
    private Coroutine imageEmptyCoroutine;
    private Coroutine imageFillCoroutine;

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
        isTrueClicked = false;

        StopTargetCoroutine(imageEmptyCoroutine);

        imageEmptyCoroutine = StartCoroutine(EmptyImage());
    }

    private void OnFalseClicked()
    {
        StopTargetCoroutine(imageFillCoroutine);
        StopTargetCoroutine(imageEmptyCoroutine);

        imageFillCoroutine = StartCoroutine(FillImage(fillImageTime));
    }

    private void OnTrueClicked()
    {
        isTrueClicked = true;
    }

    private IEnumerator EmptyImage()
    {
        image.fillAmount = 1;

        frames = 60 * eventLauncher.SecondToOneBit;
        barStep = 1 / frames;

        while (image.fillAmount > 0)
        {
            yield return null;
            image.fillAmount -= barStep;
        }

        image.fillAmount = 0;
        Debug.Log("image.fillAmount = 0");
        yield return new WaitForSeconds(additionalDelayBeforeTimeOut);
        Debug.Log("additionalDelayBeforeTimeOut");
        if (isTrueClicked == false && buttonState.IsTrueState)
        {
            TimeOut?.Invoke();
            StartCoroutine(FillImage(fillImageTime));
        }
    }

    private IEnumerator FillImage(float time)
    {
        Debug.Log("FillImage");
        var frames = 60 * time;
        var barStep = 1 / frames;

        while (image.fillAmount < 1)
        {
            yield return null;
            image.fillAmount += barStep;
        }

        image.fillAmount = 1;
    }

    private void StopTargetCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}
