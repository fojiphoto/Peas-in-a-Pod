using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TimerText;

    private void OnEnable()
    {
        GameEvents.TimerEvents.TimerUpdate.Register(OnTimerUpdate);
    }

    private void OnDisable()
    {
        GameEvents.TimerEvents.TimerUpdate.Register(OnTimerUpdate);
    }

    private void OnTimerUpdate(float value)
    {
        m_TimerText.text = $"{value}";
    }
}
