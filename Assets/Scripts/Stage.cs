using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Requiered for serializable

[Serializable]
public class Level
{
    [Range(1,26)]
    public int minValue = 1;

    [Range(1, 26)]
    public int maxValue = 1;
}

[CreateAssetMenu(fileName = "New Stage")]
public class Stage : ScriptableObject { // Scriptable object
    [Range(-100, 320)]
    public float goalMesafe = 0;
    public Color stageBackgroundMaterial = Color.magenta;
    public Color stageBackgroundColor = Color.white;
    public Color stageLevelPartColor = Color.white;
    public Color stageBallColor = Color.white;
    public Color deadPartColor = Color.red;
    public List<Level> levels = new List<Level>();
}
