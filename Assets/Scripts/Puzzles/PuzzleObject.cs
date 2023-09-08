using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle", menuName = "Puzzle/Create")]
public class PuzzleObject : ScriptableObject
{
    [field: SerializeField] public PuzzleConfig PuzzleConfig;

    public int TotalCells => PuzzleConfig.Rows * PuzzleConfig.Columns;
}
