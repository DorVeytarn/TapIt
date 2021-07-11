using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;


public class UIMoveSequence : MonoBehaviour
{
    [SerializeField] private bool onEnableStart;
    [SerializeField] private List<DOAnchorPosBaseData> sequenceElements = new List<DOAnchorPosBaseData>();

    private Sequence sequence;
    private bool isSequenceInitialized;

    private void OnEnable()
    {
        if (onEnableStart)
            Move();
    }

    public void Move()
    {
        Move(null);
    }

    public void Move(Action callback = null)
    {
        if (isSequenceInitialized == false)
            InitializeSequence();

        if (callback != null)
        {
            sequence.AppendCallback(() => callback?.Invoke());
        }

        sequence.Play();

        isSequenceInitialized = false;
    }

    public void AppendCallback(Action callback)
    {
        if(sequence != null)
        {
            sequence.AppendCallback(() => callback?.Invoke());
        }
    }

    private void InitializeSequence()
    {
        sequence = DOTween.Sequence();

        for (int i = 0; i < sequenceElements.Count; i++)
        {
            var target = sequenceElements[i];
            var tween = target.RectTransform.DOAnchorPos(target.To, target.Duration)
                                            .From(target.From)
                                            .SetEase(target.Curve)
                                            .SetDelay(target.Delay);

            sequence.Append(tween);
        }
        sequence.Pause();

        isSequenceInitialized = true;
    }
}
