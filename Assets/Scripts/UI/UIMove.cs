using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class UIMove : MonoBehaviour
{
    [Header("Tween Settings")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 from;
    [SerializeField] private Vector2 to;
    [SerializeField] private float duration;
    [SerializeField] private float delay;
    [SerializeField] private AnimationCurve curve;

    [Header("Global Settings")]
    [SerializeField] private bool onEnableStart;


    private void OnEnable()
    {
        if (onEnableStart)
            Move();
    }

    public void Move(bool forward = true, Action callback = null)
    {
        Vector2 start = (forward) ? from : to;
        Vector2 finish = (forward) ? to : from;

        var tweener = rectTransform.DOAnchorPos(start, duration).From(finish).SetEase(curve).SetDelay(delay);

        if(callback != null)
            tweener.OnComplete(() => callback?.Invoke());
    }
}
