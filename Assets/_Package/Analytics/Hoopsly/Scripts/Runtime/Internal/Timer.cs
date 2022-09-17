using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hoopsly.Internal.Time
{
    public class Timer
    {
        private bool m_isCounting = false;
        public bool IsTimerCounting { get { return m_isCounting; } }
        private string m_timerName = "";
        public string TimerName { get { return m_timerName; } }

        private int m_beginTime;
        private int m_currentDuration = 0;
        public int CurrentDuration
        {
            get { return m_currentDuration; }
        }


        public Timer(bool startNewTimerInInit = false, string timerName = "") 
        {
            m_timerName = timerName;
            if (startNewTimerInInit)
                ResetTimer();
        }

        public void ResetTimer()
        {
            m_currentDuration = 0;
            m_beginTime = 0;
            m_isCounting = false;
        }

        public void PlayTimer()
        {
            m_beginTime = TimeUtils.GetUnixTime();
            m_isCounting = true;
        }

        public void PauseTimer()
        {
            if (m_beginTime>0)
            {
                int pauseTime = TimeUtils.GetUnixTime();
                m_currentDuration = m_currentDuration + (pauseTime - m_beginTime);
            }
            m_isCounting = false;
        }

        public int FinishTimerGetTime()
        {
            PauseTimer();
            m_isCounting = false;
            int timerDuration = m_currentDuration;
            ResetTimer();
            return timerDuration;
        }
    }
}
