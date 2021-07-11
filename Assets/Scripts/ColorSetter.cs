using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSetter : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private SoundBpmEventLauncher eventLauncher;

    private Color[] colors = new Color[]
    {   Color.red,
        Color.green,
        Color.blue,
        Color.white,
        Color.black,
        Color.gray
    };

    private void Start()
    {
        eventLauncher.BitPlayed += OnBitPlayed;
    }

    private void OnDestroy()
    {
        eventLauncher.BitPlayed -= OnBitPlayed;
    }

    public void ChangeColor()
    {
        Color newColor;

        do
        {
            newColor = colors[Random.Range(0, colors.Length)];
        } 
        while (image.color == newColor);

        image.color = newColor;
    }

    private void OnBitPlayed()
    {
        ChangeColor();
    }
}
