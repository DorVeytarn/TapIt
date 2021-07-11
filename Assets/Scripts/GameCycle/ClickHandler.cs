using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private ButtonStateSetter buttonStateSetter;
    [SerializeField] private SoundBpmEventLauncher eventLauncher;
    [SerializeField] private Button button;

    private bool isNextState;

    public event Action TrueClicked;
    public event Action FalseClicked;

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClick);
        eventLauncher.BitPlayed += OnBitPlayed;
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClick);
        eventLauncher.BitPlayed -= OnBitPlayed;
    }

    private void OnButtonClick()
    {
        if (isNextState == false)
            return;

        if (buttonStateSetter.IsTrueState)
            TrueClicked?.Invoke();
        else
            FalseClicked?.Invoke();

        isNextState = false;
    }

    private void OnBitPlayed()
    {
        isNextState = true;
    }
}
