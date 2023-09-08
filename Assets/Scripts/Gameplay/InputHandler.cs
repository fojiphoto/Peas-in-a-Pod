using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_CanvasGroup;

    private void OnEnable()
    {
        GameEvents.GameplayEvents.InputStatusChanged.Register(OnInputStateChanged);
    }

    private void OnDisable()
    {
        GameEvents.GameplayEvents.InputStatusChanged.UnRegister(OnInputStateChanged);
    }

    private void OnInputStateChanged(bool status)
    {
        m_CanvasGroup.interactable = status;
    }
}
