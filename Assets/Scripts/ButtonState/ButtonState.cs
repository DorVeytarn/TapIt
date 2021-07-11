using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonState
{
    protected object falseState;
    protected Image image;

    public abstract void NextState(bool isTrueState);
    public abstract void SetFalseState(object state);
}