using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStateSetter : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private SoundBpmEventLauncher eventLauncher;
    [SerializeField] private int[] chanseNumberSeries;

    private ButtonState currentState;
    private ButtonStateChanseCalculator chanseCalculator;

    public bool IsTrueState { get; private set; }

    private void Start()
    {
        eventLauncher.BitPlayed += OnBitPlayed;
    }

    private void OnDestroy()
    {
        eventLauncher.BitPlayed -= OnBitPlayed;
    }

    public void SetColorState(List<Color> colors, Color falseColor)
    {
        currentState = new ButtonColorState(colors, image);
        chanseCalculator = new ButtonStateChanseCalculator(chanseNumberSeries);

        currentState.SetFalseState(falseColor);
    }

    private void OnBitPlayed()
    {
        if(currentState != null)
        {
            IsTrueState = chanseCalculator.CalculateChanse();
            currentState.NextState(IsTrueState);
        }
    }
}
