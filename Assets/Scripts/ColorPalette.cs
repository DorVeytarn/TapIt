using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ColorPalette : ScriptableObject
{
    [SerializeField] private List<Color> colors = new List<Color>();

    public List<Color> Colors => colors;
}
