using UnityEngine;

[System.Serializable]
public class DOAnchorPosBaseData
{
    [Header("Tween Settings")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 from;
    [SerializeField] private Vector2 to;
    [SerializeField] private float duration;
    [SerializeField] private float delay;
    [SerializeField] private AnimationCurve curve;

    public RectTransform RectTransform => rectTransform;
    public Vector2 From => from;
    public Vector2 To => to;
    public float Duration => duration;
    public float Delay => delay;
    public AnimationCurve Curve => curve;
}
