using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorState : ButtonState
{
    protected List<Color> states;
    private Color falseColorState;

    public ButtonColorState(List<Color> states, Image image)
    {
        this.states = states;
        this.image = image;
    }
  
    public override void NextState(bool isTrueState)
    {
        Color newColor;

        if (isTrueState)
        {
            do
            {
                newColor = states[Random.Range(0, states.Count)];
            }
            while (image.color.Equals(newColor) || newColor.Equals(falseColorState));
        }
        else
            newColor = falseColorState;

        //Debug.Log($"New color - {newColor} is {isTrueState} state, false state: {falseColorState}");
        image.color = newColor;
    }

    public override void SetFalseState(object state)
    {
        if(state is Color)
            falseColorState = (Color)state;
    }
}
