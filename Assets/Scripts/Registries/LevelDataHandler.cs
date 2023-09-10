using System.Collections.Generic;
using UnityEngine;

public class LevelDataHandler : MonoBehaviour
{
    [SerializeField] private List<PuzzleObject> m_Items = new();

    public PuzzleObject GetPuzzleData(PuzzleConfig PuzzleConfig) => m_Items.Find(x => x.PuzzleConfig == PuzzleConfig);
}
