using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float m_DurationForGame = 30f;
    
    private float m_InternalTimer;
    
    private WaitForSeconds m_SecondsDelay;
    private Coroutine m_Routine;

    private void Start()
    {
        m_SecondsDelay = new WaitForSeconds(1f);
    }

    private void OnEnable()
    {
        m_InternalTimer = m_DurationForGame;
        
        GameEvents.GameplayEvents.StartGame.Register(StartTimer);
        GameEvents.GameplayEvents.GameComplete.Register(OnGameComplete);
    }

    private void OnDisable()
    {
        GameEvents.GameplayEvents.StartGame.UnRegister(StartTimer);
        GameEvents.GameplayEvents.GameComplete.UnRegister(OnGameComplete);
    }

    private void OnGameComplete(bool status)
    {
        StopTimer();
    }
    
    private void StartTimer()
    {
        m_Routine = StartCoroutine(TimerRoutine());
    }

    private void StopTimer()
    {
        if (m_Routine == null)
            return;
        
        StopCoroutine(m_Routine);
        m_Routine = null;
    }

    private IEnumerator TimerRoutine()
    {
        while (m_InternalTimer > 0)
        {
            yield return m_SecondsDelay;
            m_InternalTimer--;
            GameEvents.TimerEvents.TimerUpdate.Raise(m_InternalTimer);
        }
        GameEvents.TimerEvents.TimerComplete.Raise();
    }
}
