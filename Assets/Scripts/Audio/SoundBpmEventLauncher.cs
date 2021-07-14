using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBpmEventLauncher : MonoBehaviour
{
    private const int seconds = 60;

    [SerializeField] private float delayBeforeEvent;
    [SerializeField] private AudioPlayer audioPlayer;

    private int currentBpm;
    private Coroutine bittingCoroutine;
    private Coroutine evaluatingBitCoroutine;
    private AnimationCurve bittingCurve;

    public float SecondToOneBit { get; private set; }

    public event Action BitPlayed;

    public void StartBitCalling(float bpm, AnimationCurve curve = null)
    {
        if (curve != null)
        {
            if (evaluatingBitCoroutine != null)
                StopCoroutine(evaluatingBitCoroutine);

            evaluatingBitCoroutine = StartCoroutine(EvaluatingBitCurve(bpm, curve));
        }
        else
            SecondToOneBit = 1f / (bpm / seconds);

        if (bittingCoroutine != null)
            StopCoroutine(bittingCoroutine);

        bittingCoroutine = StartCoroutine(BitEventCaller());
    }


    public void StopBitCalling()
    {
        if (bittingCoroutine != null)
            StopCoroutine(bittingCoroutine);

        if (evaluatingBitCoroutine != null)
            StopCoroutine(evaluatingBitCoroutine);
    }

    private IEnumerator BitEventCaller()
    {
        while (true)
        {
            BitPlayed?.Invoke();
            audioPlayer.Play();
            //Debug.Log(SecondToOneBit);
            yield return new WaitForSeconds(SecondToOneBit);
        }
    }

    private IEnumerator EvaluatingBitCurve(float bpm, AnimationCurve curve)
    {
        curve.postWrapMode = WrapMode.Loop;
        float time = 0;

        while (true)
        {
            SecondToOneBit = 1f / (bpm * curve.Evaluate(time) / seconds);

            yield return null;

            time += Time.deltaTime;
        }
    }
}
